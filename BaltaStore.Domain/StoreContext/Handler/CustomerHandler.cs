using System;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using BaltaStore.Domain.StoreContext.Entities;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Domain.StoreContext.Services;
using BaltaStore.Domain.StoreContext.ValueObjects;
using BaltaStore.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;

namespace BaltaStore.Domain.StoreContext.Handler {
    public class CustomerHandler : Notifiable, ICommandHandler<CreateCustomerCommand>, ICommandHandler<AddAddressCommand> {
        private readonly ICustomerRepository _repository;
        private readonly IEmailService _emailService;
        
        public CustomerHandler(ICustomerRepository repository, IEmailService emailService) {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateCustomerCommand command) {
            //1) Verificar se existe CPF, email
            if(_repository.CheckDocument(command.Document)) {
                AddNotification("Document", "CPF informado já está em uso!");
            }

            if(_repository.CheckEmail(command.Email)) {
                AddNotification("Email", "Email informado já está em uso!");
            }

            //2) Criar os VO's
            var name = new Name(command.FirstName, command.LastName);
            var doc = new Document(command.Document);
            var email = new Email(command.Email);
            
            //3) Criar a entidade
            var customer = new Customer(name, doc, email, command.Phone);
            
            //4) Validar a entidade
            AddNotifications(name.Notifications);
            AddNotifications(doc.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if(Invalid) return null;

            //5) Persistir o cliente
            _repository.Save(customer);

            //6) Enviar o email de boas vindas
            _emailService.Send(email.Address, "hello@gmail.com", "Bem vindo", "Seja bem vindo ao Balta");

            //7) Retornar o resultado para a tela
            return new CreateCustomerCommandResult(customer.Id, customer.ToString(), customer.Document.ToString(), customer.Email.Address);
        }
        public ICommandResult Handle(AddAddressCommand command) {
            return new AddAddressCommandResult();
        }
    }
}