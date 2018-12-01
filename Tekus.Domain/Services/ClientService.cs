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
    public interface IClientService : IService
    {
        Task<List<ClientModel>> GetClients(bool withServices, bool withServicesCountries);
        Task<List<ClientModel>> GetEnabledClients(bool withServices, bool withServicesCountries);
        Task<ResponseModel<long>> Save(ClientModel clientModel);
        Task<ResponseModel<bool>> SetEnabledState(long clientId);
    }
    public class ClientService : IClientService
    {
        private IClientRepository _clientRepository;
        private IUnitOfWork _unitOfWork;

        public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<bool>> SetEnabledState(long clientId)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var client = await _clientRepository.Load(clientId);
                client.IsEnabled = !client.IsEnabled;

                using (_unitOfWork)
                {
                    await _clientRepository.Save(client);
                }

                response.Data = client.IsEnabled;
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

        public async Task<List<ClientModel>> GetClients(bool withServices, bool withServicesCountries)
        {
            var clients = await _clientRepository.GetAll();
            return ClientModel.MakeMany(clients.ToList(), withServices, withServicesCountries);
        }

        public async Task<List<ClientModel>> GetEnabledClients(bool withServices, bool withServicesCountries)
        {
            var clients = await _clientRepository.GetEnabledClients();
            return ClientModel.MakeMany(clients.ToList(), withServices, withServicesCountries);
        }

        public async Task<ResponseModel<long>> Save(ClientModel clientModel)
        {
            var response = new ResponseModel<long>();

            try
            {
                // It's check if the client Name already exist into database 
                // If client exist then it's sent a fault response                
                var nameIsValid = await _clientRepository.ValidateIfClientNameIsUnique(clientModel.Name);
                if (!nameIsValid)
                {
                    response.Success = false;
                    response.Messages.Add($"Ya existe un cliente con el nombre '{clientModel.Name}'");
                    return response;
                }

                // It's check if the client NIT already exist into database 
                // If client exist then it's sent a fault response       
                var nitIsvalid = await _clientRepository.ValidateIfClientNitIsUnique(clientModel.NIT);
                if (!nitIsvalid)
                {
                    response.Success = false;
                    response.Messages.Add($"Ya existe un cliente con el NIT '{clientModel.NIT}'");
                    return response;
                }

                // It's sent the client model info to the client entity object
                var client = new Client();
                clientModel.FillUp(client);

                using (_unitOfWork)
                {
                    await _clientRepository.Save(client);
                }

                response.Success = true;
                response.Data = client.Id;
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
