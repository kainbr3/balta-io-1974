using System;
using BaltaStore.Shared.Commands;

namespace BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs {
    public class CreateCustomerCommandResult : ICommandResult {
        public CreateCustomerCommandResult() {}

        public CreateCustomerCommandResult(Guid id, string name, string document, string email) {
            Id = id;
            Name = name;
            Document = document;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
    }
}