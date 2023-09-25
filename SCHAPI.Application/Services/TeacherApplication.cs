using AutoMapper;
using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Teacher.Request;
using SCHAPI.Application.Dtos.Teacher.Response;
using SCHAPI.Application.Interfaces;
using SCHAPI.Application.Validators.Teacher;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Interfaces;
using SCHAPI.Utilities.Static;
using WatchDog;

namespace SCHAPI.Application.Services
{
    public class TeacherApplication : ITeacherApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly TeacherRequestValidator _requestValidation;
        private readonly TeacherEntityValidator _entityValidation;

        public TeacherApplication(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            TeacherRequestValidator requestValidation,
            TeacherEntityValidator entityValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestValidation = requestValidation;
            _entityValidation = entityValidation;
        }

        public async Task<BaseResponse<BaseEntityResponse<TeacherResponseDto>>> ListTeachers(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<TeacherResponseDto>>();

            try
            {
                var teachers = await _unitOfWork.Teacher.ListTeachers(filters);

                if (teachers.Items!.Any())
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<TeacherResponseDto>>(teachers);
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

        public async Task<BaseResponse<IEnumerable<TeacherSelectResponseDto>>> ListSelectTeachers()
        {
            var response = new BaseResponse<IEnumerable<TeacherSelectResponseDto>>();

            try
            {
                var teachers = await _unitOfWork.Teacher.GetAllAsync();

                if (teachers.Any())
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<TeacherSelectResponseDto>>(teachers);
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

        public async Task<BaseResponse<TeacherResponseDto>> TeacherById(int teacherId)
        {
            var response = new BaseResponse<TeacherResponseDto>();

            try
            {
                var teacher = await _unitOfWork.Teacher.GetByIdAsync(teacherId);

                if (teacher != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<TeacherResponseDto>(teacher);
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

        public async Task<BaseResponse<bool>> RegisterTeacher(TeacherRequestDto requestDto)
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

                var teacher = _mapper.Map<Teacher>(requestDto);

                validationResult = await _entityValidation.ValidateAsync(teacher);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Teacher.RegisterAsync(teacher);
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

        public async Task<BaseResponse<bool>> EditTeacher(int teacherId, TeacherRequestDto requestDto)
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

                var teacherEdit = await _unitOfWork.Teacher.GetByIdAsync(teacherId);
                if (teacherEdit == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                var teacher = _mapper.Map<Teacher>(requestDto);
                teacher.Id = teacherId;

                validationResult = await _entityValidation.ValidateAsync(teacher);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Teacher.EditAsync(teacher);
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

        public async Task<BaseResponse<bool>> RemoveTeacher(int teacherId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var teacher = await TeacherById(teacherId);
                if (teacher.Data == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                response.Data = await _unitOfWork.Teacher.RemoveAsync(teacherId);
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
