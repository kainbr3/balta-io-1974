using System;
using BaltaStore.Domain.StoreContext.Enums;
using BaltaStore.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;

namespace BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs {
    public class AddAddressCommand : Notifiable, ICommand {
        public AddAddressCommand() {
            AddNotifications(new ValidationContract()
                .HasLen(Id.ToString(), 36, "Identificador", "Indentificador inválido")
            );
        }

        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public EAddressType Type { get; set; }

        public bool Validated() {
            return (Notifications.Count == 0);
        }
    }
}