using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Subject.Request;
using SCHAPI.Application.Dtos.Subject.Response;
using SCHAPI.Application.Interfaces;
using SCHAPI.Application.Validators.Subject;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Interfaces;
using SCHAPI.Utilities.Static;
using WatchDog;

namespace SCHAPI.Application.Services
{
    public class SubjectApplication : ISubjectApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SubjectRequestValidator _requestValidation;
        private readonly SubjectEntityValidator _entityValidation;

        public SubjectApplication(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            SubjectRequestValidator requestValidation,
            SubjectEntityValidator entityValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestValidation = requestValidation;
            _entityValidation = entityValidation;
        }

        public async Task<BaseResponse<BaseEntityResponse<SubjectResponseDto>>> ListSubjects(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<SubjectResponseDto>>();

            try
            {
                var subjects = await _unitOfWork.Subject.ListSubjects(filters);

                if (subjects != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<SubjectResponseDto>>(subjects);
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

        public async Task<BaseResponse<IEnumerable<SubjectSelectResponseDto>>> ListSelectSubjects()
        {
            var response = new BaseResponse<IEnumerable<SubjectSelectResponseDto>>();

            try
            {
                var subjects = await _unitOfWork.Subject.GetAllAsync();

                if (subjects != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<SubjectSelectResponseDto>>(subjects);
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

        public async Task<BaseResponse<SubjectResponseDto>> SubjectById(int subjectId)
        {
            var response = new BaseResponse<SubjectResponseDto>();

            try
            {
                var subject = await _unitOfWork.Subject.GetByIdAsync(subjectId);

                if (subject != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<SubjectResponseDto>(subject);
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

        public async Task<BaseResponse<bool>> RegisterSubject(SubjectRequestDto requestDto)
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

                var subject = _mapper.Map<Subject>(requestDto);

                validationResult = await _entityValidation.ValidateAsync(subject);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Subject.RegisterAsync(subject);
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

        public async Task<BaseResponse<bool>> EditSubject(int subjectId, SubjectRequestDto requestDto)
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

                var subjectEdit = await _unitOfWork.Subject.GetByIdAsync(subjectId);
                if (subjectEdit == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                var subject = _mapper.Map<Subject>(requestDto);
                subject.Id = subjectId;

                validationResult = await _entityValidation.ValidateAsync(subject);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Subject.EditAsync(subject);
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

        public async Task<BaseResponse<bool>> RemoveSubject(int subjectId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var subject = await _unitOfWork.Subject.GetByIdAsync(subjectId);

                if (subject == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                response.Data = await _unitOfWork.Subject.RemoveAsync(subjectId);

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
