using AutoMapper;
using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Classroom.Request;
using SCHAPI.Application.Dtos.Classroom.Response;
using SCHAPI.Application.Interfaces;
using SCHAPI.Application.Validators.Classroom;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Interfaces;
using SCHAPI.Utilities.Static;
using WatchDog;

namespace SCHAPI.Application.Services
{
    public class ClassroomApplication : IClassroomApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ClassroomRequestValidator _requestValidation;
        private readonly ClassroomEntityValidator _entityValidation;

        public ClassroomApplication(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ClassroomRequestValidator requestValidation,
            ClassroomEntityValidator entityValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestValidation = requestValidation;
            _entityValidation = entityValidation;
        }

        public async Task<BaseResponse<BaseEntityResponse<ClassroomResponseDto>>> ListClassrooms(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ClassroomResponseDto>>();

            try
            {
                var classrooms = await _unitOfWork.Classroom.ListClassrooms(filters);

                if (classrooms != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<ClassroomResponseDto>>(classrooms);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;

                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<ClassroomSelectResponseDto>>> ListSelectClassrooms()
        {
            var response = new BaseResponse<IEnumerable<ClassroomSelectResponseDto>>();

            try
            {
                var classrooms = await _unitOfWork.Classroom.GetAllAsync();

                if (classrooms != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<ClassroomSelectResponseDto>>(classrooms);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;

                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<ClassroomResponseDto>> ClassroomById(int classroomId)
        {
            var response = new BaseResponse<ClassroomResponseDto>();

            try
            {
                var classroom = await _unitOfWork.Classroom.GetByIdAsync(classroomId);

                if (classroom != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<ClassroomResponseDto>(classroom);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;

                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterClassroom(ClassroomRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var validationResult = await _requestValidation.ValidateAsync(requestDto);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                var classroom = _mapper.Map<Classroom>(requestDto);

                validationResult = await _entityValidation.ValidateAsync(classroom);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Classroom.RegisterAsync(classroom);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;

                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditClassroom(int classroomId, ClassroomRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var validationResult = await _requestValidation.ValidateAsync(requestDto);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                var classroomEdit = await _unitOfWork.Classroom.GetByIdAsync(classroomId);
                if (classroomEdit == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                var classroom = _mapper.Map<Classroom>(requestDto);
                classroom.Id = classroomId;

                validationResult = await _entityValidation.ValidateAsync(classroom);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Classroom.EditAsync(classroom);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_UPDATE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;

                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveClassroom(int classroomId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var classroom = await _unitOfWork.Classroom.GetByIdAsync(classroomId);

                if (classroom == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                response.Data = await _unitOfWork.Classroom.RemoveAsync(classroomId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;

                WatchLogger.Log(ex.Message);
            }

            return response;
        }
    }
}
