using System;
using FluentValidator;

namespace BaltaStore.Shared.Entities {
    public abstract class Entity : Notifiable {
        public Guid Id { get; private set; }
    }
}