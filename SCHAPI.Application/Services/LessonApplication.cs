using AutoMapper;
using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Lesson.Request;
using SCHAPI.Application.Dtos.Lesson.Response;
using SCHAPI.Application.Interfaces;
using SCHAPI.Application.Validators.Lesson;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Interfaces;
using SCHAPI.Utilities.Static;
using WatchDog;

namespace SCHAPI.Application.Services
{
    public class LessonApplication : ILessonApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LessonRequestValidator _requestValidation;
        private readonly LessonEntityValidator _entityValidation;

        public LessonApplication(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            LessonRequestValidator requestValidation,
            LessonEntityValidator entityValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _entityValidation = entityValidation;
            _requestValidation = requestValidation;
        }

        public async Task<BaseResponse<BaseEntityResponse<LessonResponseDto>>> ListLessons(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<LessonResponseDto>>();

            try
            {
                var lessons = await _unitOfWork.Lesson.ListLessons(filters);

                if (lessons.Items!.Any())
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<LessonResponseDto>>(lessons);
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

        public async Task<BaseResponse<IEnumerable<LessonSelectResponseDto>>> ListSelectLessons()
        {
            var response = new BaseResponse<IEnumerable<LessonSelectResponseDto>>();

            try
            {
                var lessons = await _unitOfWork.Lesson.ListSelectLessons();

                if (lessons.Any())
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<LessonSelectResponseDto>>(lessons);
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

        public async Task<BaseResponse<LessonResponseDto>> LessonById(int lessonId)
        {
            var response = new BaseResponse<LessonResponseDto>();

            try
            {
                var lesson = await _unitOfWork.Lesson.GetByIdAsync(lessonId);

                if (lesson != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<LessonResponseDto>(lesson);
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

        public async Task<BaseResponse<bool>> RegisterLesson(LessonRequestDto requestDto)
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

                var lesson = _mapper.Map<Lesson>(requestDto);

                validationResult = await _entityValidation.ValidateAsync(lesson);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Lesson.RegisterAsync(lesson);
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

        public async Task<BaseResponse<bool>> EditLesson(int lessonId, LessonRequestDto requestDto)
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

                var lessonEdit = await _unitOfWork.Lesson.GetByIdAsync(lessonId);
                if (lessonEdit == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                var lesson = _mapper.Map<Lesson>(requestDto);
                lesson.Id = lessonId;

                validationResult = await _entityValidation.ValidateAsync(lesson);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Lesson.EditAsync(lesson);
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

        public async Task<BaseResponse<bool>> RemoveLesson(int lessonId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var lesson = await _unitOfWork.Lesson.GetByIdAsync(lessonId);

                if (lesson == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                response.Data = await _unitOfWork.Lesson.RemoveAsync(lessonId);
                
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
