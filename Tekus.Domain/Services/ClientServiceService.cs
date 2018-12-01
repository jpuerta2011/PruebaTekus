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
    public interface IClientServiceService : IService
    {
        Task<List<ClientServiceModel>> GetAllClientServiceByClientId(long clientId);
        Task<List<ClientServiceModel>> GetAllClientServiceByServiceId(long serviceId);
        Task<ResponseModel<long>> Save(ClientServiceModel clientServiceModel);
        Task<ResponseModel<long>> Delete(long clientServiceId);
        Task<List<ClientServiceModel>> GetAllClientServiceByClientIdWithCountries(long clientId);
    }
    public class ClientServiceService : IClientServiceService
    {

        private IClientServiceRepository _clientServiceRepository;
        private IClientServiceCountryRepository _clientServiceCountryRepository;
        private IClientRepository _clientRepository;
        private IServiceRepository _serviceRepository;
        private IUnitOfWork _unitOfWork;
        private ICountryRepository _countryRepository;

        public ClientServiceService(IClientServiceRepository clientServiceRepository, IClientServiceCountryRepository clientServiceCountryRepository, 
            IUnitOfWork unitOfWork, IClientRepository clientRepository, IServiceRepository serviceRepository, ICountryRepository countryRepository)
        {
            _clientServiceRepository = clientServiceRepository;
            _clientServiceCountryRepository = clientServiceCountryRepository;
            _unitOfWork = unitOfWork;
            _clientRepository = clientRepository;
            _serviceRepository = serviceRepository;
            _countryRepository = countryRepository;
        }

        public async Task<ResponseModel<long>> Delete(long clientServiceId)
        {
            var response = new ResponseModel<long>();

            try
            {
                var countries = await _clientServiceCountryRepository.GetClientServiceCountriesByClientServiceId(clientServiceId);
                var clientService = await _clientServiceRepository.Load(clientServiceId);

                using (_unitOfWork)
                {
                    // It's delete the all countries for the ClientService befor that delete the ClientService
                    foreach (var country in countries)
                    {
                        _clientServiceCountryRepository.Remove(country);
                    }

                    _clientServiceRepository.Remove(clientService);
                }

                response.Success = true;
                response.Data = clientServiceId;
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


        public async Task<List<ClientServiceModel>> GetAllClientServiceByClientId(long clientId)
        {
            var clientServices = await _clientServiceRepository.GetAllClientServiceByClientId(clientId);
            return ClientServiceModel.MakeMany(clientServices, false);
        }

        public async Task<List<ClientServiceModel>> GetAllClientServiceByClientIdWithCountries(long clientId)
        {
            var clientServices = await _clientServiceRepository.GetAllClientServiceByClientId(clientId);
            return ClientServiceModel.MakeMany(clientServices, true);
        }

        public async Task<List<ClientServiceModel>> GetAllClientServiceByServiceId(long serviceId)
        {
            var clientServices = await _clientServiceRepository.GetAllClientServiceByServiceId(serviceId);
            return ClientServiceModel.MakeMany(clientServices, false);
        }

        public async Task<List<ClientServiceModel>> GetAllClientServiceByServiceIdWithCountries(long serviceId)
        {
            var clientServices = await _clientServiceRepository.GetAllClientServiceByServiceId(serviceId);
            return ClientServiceModel.MakeMany(clientServices, true);
        }

        public async Task<ResponseModel<long>> Save(ClientServiceModel clientServiceModel)
        {
            var response = new ResponseModel<long>();

            try
            {
                // It's check if the service client already exist into database 
                // If the service client exist then it's sent a fault response        
                var validClientService = await _clientServiceRepository.ValidateIfClientServiceIsUnique(clientServiceModel.ClientId, clientServiceModel.ServiceId);
                if (!validClientService)
                {
                    response.Success = false;
                    response.Messages.Add($"Ya esta asociado el servicio al cliente");
                    return response;
                }

                var clientService = new Data.Entities.ClientService();
                clientServiceModel.FillUp(clientService);
                clientService.Client = await _clientRepository.Load(clientServiceModel.ClientId);
                clientService.Service = await _serviceRepository.Load(clientServiceModel.ServiceId);

                using (_unitOfWork)
                {
                    await _clientServiceRepository.Save(clientService);
                    foreach (var countryModel in clientServiceModel.Countries)
                    {
                        var country = await _countryRepository.Load(countryModel.Id);
                        var clientServiceCountry = new ClientServiceCountry();
                        clientServiceCountry.Country = country;
                        clientServiceCountry.ClientService = clientService;
                        await _clientServiceCountryRepository.Save(clientServiceCountry);
                    }
                }

                response.Success = true;
                response.Data = clientService.Id;
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
    }
}
