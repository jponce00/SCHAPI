using AutoMapper;
using SCHAPI.Application.Commons.Bases;
using SCHAPI.Application.Dtos.Student.Request;
using SCHAPI.Application.Dtos.Student.Response;
using SCHAPI.Application.Interfaces;
using SCHAPI.Application.Validators.Student;
using SCHAPI.Domain.Entities;
using SCHAPI.Infrastructure.Commons.Bases.Request;
using SCHAPI.Infrastructure.Commons.Bases.Response;
using SCHAPI.Infrastructure.Persistences.Interfaces;
using SCHAPI.Utilities.Static;
using WatchDog;

namespace SCHAPI.Application.Services
{
    public class StudentApplication : IStudentApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StudentRequestValidator _requestValidation;
        private readonly StudentEntityValidator _entityValidation;

        public StudentApplication(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            StudentRequestValidator validationRules,
            StudentEntityValidator entityValidation)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestValidation = validationRules;
            _entityValidation = entityValidation;
        }

        public async Task<BaseResponse<BaseEntityResponse<StudentResponseDto>>> ListStudents(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<StudentResponseDto>>();

            try
            {
                var students = await _unitOfWork.Student.ListStudents(filters);

                if (students.Items!.Any())
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<StudentResponseDto>>(students);
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

        public async Task<BaseResponse<StudentResponseDto>> StudentById(int studentId)
        {
            var response = new BaseResponse<StudentResponseDto>();

            try
            {
                var student = await _unitOfWork.Student.GetByIdAsync(studentId);

                if (student != null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<StudentResponseDto>(student);
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

        public async Task<BaseResponse<bool>> RegisterStudent(StudentRequestDto requestDto)
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

                var student = _mapper.Map<Student>(requestDto);

                validationResult = await _entityValidation.ValidateAsync(student);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Student.RegisterAsync(student);
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

        public async Task<BaseResponse<bool>> EditStudent(int studentId, StudentRequestDto requestDto)
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

                var studentEdit = await _unitOfWork.Student.GetByIdAsync(studentId);
                if (studentEdit == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                var student = _mapper.Map<Student>(requestDto);
                student.Id = studentId;

                validationResult = await _entityValidation.ValidateAsync(student);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                response.Data = await _unitOfWork.Student.EditAsync(student);
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

        public async Task<BaseResponse<bool>> RemoveStudent(int studentId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var student = await _unitOfWork.Student.GetByIdAsync(studentId);

                if (student == null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DOESNOT_EXIST;

                    return response;
                }

                response.Data = await _unitOfWork.Student.RemoveAsync(studentId);

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
