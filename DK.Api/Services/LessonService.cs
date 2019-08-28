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
    public class LessonService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IRepository<Lesson> _repository;

        public LessonService()
        {
            _unitOfWork = new UnitOfWork(WebApiApplication.SessionFactory);
            _repository = new CoreRepository<Lesson>(WebApiApplication.GetCurrentSession());
        }

        public int Add(Lesson entity)
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

        public bool Update(Lesson entity)
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

        public Lesson Get(int id)
        {
            Lesson result = new Lesson();

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

        public Lesson FindBy(Expression<Func<Lesson, bool>> expression)
        {
            Lesson result = new Lesson();

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

        public List<Lesson> GetAll()
        {
            List<Lesson> result = new List<Lesson>();

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

        public List<Lesson> FilterBy(Expression<Func<Lesson, bool>> expression)
        {
            List<Lesson> result = new List<Lesson>();

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