using AutoMapper;
using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.LessonStudent.Request;
using SCHAPI.Application.Dtos.LessonStudent.Response;
using SCHAPI.Application.Interfaces;
using SCHAPI.Application.Validators.LessonStudent;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Interfaces;
using SCHAPI.Utilities.Static;
using WatchDog;

namespace SCHAPI.Application.Services
{
    public class LessonStudentApplication : ILessonStudentApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LessonStudentRequestValidator _requestValidation;
        private readonly LessonStudentEntityValidator _entityValidation;

        public LessonStudentApplication(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            LessonStudentRequestValidator requestValidation,
            LessonStudentEntityValidator entityValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestValidation = requestValidation;
            _entityValidation = entityValidation;
        }

        public async Task<BaseResponse<BaseEntityResponse<LessonStudentResponseDto>>> ListLessonStudents(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<LessonStudentResponseDto>>();

            try
            {
                var lessonStudents = await _unitOfWork.LessonStudent.ListLessonStudents(filters);

                if (lessonStudents.Items!.Any())
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<LessonStudentResponseDto>>(lessonStudents);
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

        public async Task<BaseResponse<bool>> RegisterLessonStudent(LessonStudentRequestDto requestDto)
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

                var lessonStudent = _mapper.Map<LessonStudent>(requestDto);

                validationResult = await _entityValidation.ValidateAsync(lessonStudent);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.LessonStudent.RegisterAsync(lessonStudent);
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

        public async Task<BaseResponse<bool>> RemoveLessonStudents(int lessonStudentId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var lessonStudent = await _unitOfWork.LessonStudent.GetByIdAsync(lessonStudentId);

                if (lessonStudent == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                response.Data = await _unitOfWork.LessonStudent.RemoveAsync(lessonStudentId);

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
