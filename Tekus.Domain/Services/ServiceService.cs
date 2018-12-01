using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Common;
using Tekus.Common.Dependencies.UnitOfWork;
using Tekus.Data.Entities;
using Tekus.Data.Repositories.Data;
using Tekus.Domain.Models;

namespace Tekus.Domain.Services
{
    public interface IServiceService : IService
    {
        Task<List<ServiceModel>> GetServices();
        Task<List<ServiceModel>> GetEnabledServices();
        Task<ResponseModel<long>> Save(ServiceModel serviceModel);
        Task<ResponseModel<bool>> SetEnabledState(long serviceId);
    }
    public class ServiceService : IServiceService
    {
        private IServiceRepository _serviceRepository;
        private IUnitOfWork _unitOfWork;

        public ServiceService(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ServiceModel>> GetEnabledServices()
        {
            var services = await _serviceRepository.GetEnabledServices();
            return ServiceModel.MakeMany(services.ToList());
        }

        public async Task<List<ServiceModel>> GetServices()
        {
            var services = await _serviceRepository.GetAll();
            return ServiceModel.MakeMany(services.ToList());
        }

        public async Task<ResponseModel<long>> Save(ServiceModel serviceModel)
        {
            var response = new ResponseModel<long>();

            try
            {
                // It's check if the service Name already exist into database 
                // If service exist then it's sent a fault response                
                var nameIsValid = await _serviceRepository.ValidateIfServiceNameIsUnique(serviceModel.Name);
                if (!nameIsValid)
                {
                    response.Success = false;
                    response.Messages.Add($"Ya existe un servicio con el nombre '{serviceModel.Name}'");
                    return response;
                }

                // It's sent the service model info to the service entity object
                var service = new Service();
                serviceModel.FillUp(service);

                using (_unitOfWork)
                {
                    await _serviceRepository.Save(service);
                }

                response.Success = true;
                response.Data = service.Id;
            }
            catch (Exception ex)
            {
                response.Success = false;
                // When the application is in debug mode then the exception info it's sent
                // otherwise, it's sent a general message
                if (ApplicationConfiguration.Instance.DebugFlagActivated)
                {
                    response.Messages.Add($"ERROR: {ex.Message} StackTrace: {ex.StackTrace}");
                }
                else
                {
                    response.Messages.Add("Erorr en el proceso");
                }
            }

            return response;
        }

        public async Task<ResponseModel<bool>> SetEnabledState(long serviceId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var service = await _serviceRepository.Load(serviceId);
                service.IsEnabled = !service.IsEnabled;

                using (_unitOfWork)
                {
                    await _serviceRepository.Save(service);
                }

                response.Data = service.IsEnabled;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                if (ApplicationConfiguration.Instance.DebugFlagActivated)
                {
                    response.Messages.Add($"ERROR: {ex.Message} StackTrace: {ex.StackTrace}");
                }
                else
                {
                    response.Messages.Add("Erorr en el proceso");
                }
            }

            return response;
        }
    }
}
