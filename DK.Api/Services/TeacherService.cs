using DK.Dal.Entities;
using DK.Dal.Enums;
using DK.Dal.Interfaces;
using DK.Dal.Repositories;
using DK.Dal.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DK.Api.Services
{
    public class TeacherService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IRepository<Teacher> _repository;

        public TeacherService()
        {
            _unitOfWork = new UnitOfWork(WebApiApplication.SessionFactory);
            _repository = new CoreRepository<Teacher>(WebApiApplication.GetCurrentSession());
        }

        public int Add(Teacher entity)
        {
            int result = 0;

            entity.Id = 0;
            entity.CreatedOn = DateTime.UtcNow;

            try
            {
                result = _repository.Add(entity);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);

                _unitOfWork.Rollback();
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return result;
        }

        public bool Update(Teacher entity)
        {
            bool result = true;

            try
            {
                _repository.Update(entity);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);

                _unitOfWork.Rollback();
                result = false;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool result = true;

            try
            {
                var entity = _repository.FindBy(id);

                if (entity != null)
                {
                    _repository.Delete(entity);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);

                _unitOfWork.Rollback();
                result = false;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return result;
        }

        public Teacher Get(int id)
        {
            Teacher result = new Teacher();

            try
            {
                result = _repository.FindBy(id);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }

        public Teacher FindBy(Expression<Func<Teacher, bool>> expression)
        {
            Teacher result = new Teacher();

            try
            {
                result = _repository.FindBy(expression);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<Teacher> GetAll()
        {
            List<Teacher> result = new List<Teacher>();

            try
            {
                result = _repository.Items?.ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }

        public List<Teacher> FilterBy(Expression<Func<Teacher, bool>> expression)
        {
            List<Teacher> result = new List<Teacher>();

            try
            {
                result = _repository.FilterBy(expression)?.ToList();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}