using Domain.Table.Enum;
using Domain.Table.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Table
{
    public class Table
    {
        public Guid Id { get; private set; }
        public int Number { get; private set; }
        public StatusTable Status { get; private set; }
        public OrderId? OrderId { get; private set; }
        public StatusAccess StatusAccess { get; private set; }


        protected Table () {}

        public Table (int number)
        {
            if (number <= 0) 
                throw new ArgumentException("Número da mesa inválido.");

            Id = Guid.NewGuid();
            Number = number;
            Status = StatusTable.Livre;
            StatusAccess = StatusAccess.Unlocked();
        }

        
        public void CreateOrder (OrderId orderId, UserId userId)
        {
            EnsuereUserHasLocked(userId);
            EnsureNotClosed();
            EnsureNotOccupied();
            CreateOrderId(orderId);
            Occuppy();
        }

        public void FinishOrder()
        {
            EnsureIsOrder();
            EnsureIsLocked();
            EnsureNotClosed();

    
            OrderId = null;
            Status = StatusTable.Livre;

        }

        public void DeleteTable ()
        {
            EnsureNotOrder();
            EnsureNotOccupied();

        }

        public void LockedAcess(UserId userId)
        {
            if (userId is null)
                throw new InvalidOperationException("Id úsuario inválido.");

            StatusAccess = StatusAccess.Locked(userId);
        }

        public void UnlockedAcess(UserId userId)
        {
            if (userId is null)
                throw new InvalidOperationException("Id usuário inválido");

            if (StatusAccess.IsSameUser(userId) is false)
                throw new InvalidOperationException("Usuário diferente não pode desbloquear o acesso à mesa.");

            StatusAccess = StatusAccess.Unlocked();
        }


        private void CreateOrderId (OrderId orderId)
        {
            if (OrderId is null)
                throw new ArgumentException("Id inválido.");

            OrderId = orderId;
        }
        


        public void Close()
        {
            EnsureNotClosed();
            EnsureNotOccupied();
            EnsureIsOrder();

            Status = StatusTable.Fechada;

        }

        public void Open()
        {
            EnsureIsOpen();

            Status = StatusTable.Livre;
        }

        private void Occuppy()
        {
            EnsureNotClosed();
            EnsureNotOccupied();
            Status = StatusTable.Ocupada;
        }


        //ENSURE
        private void EnsureNotClosed()
        {
            if (Status is StatusTable.Fechada)
                throw new InvalidOperationException("Mesa está fechada.");
        }

        private void EnsureNotOccupied()
        {
            if (Status is StatusTable.Ocupada && OrderId is not null)
                throw new InvalidOperationException("Mesa já está ocupada."); 
        }

        private void EnsureIsOpen()
        {
            if (Status != StatusTable.Fechada)
                throw new InvalidOperationException("Mesa já está aberta.");
        }

        private void EnsureNotOrder()
        {
            if (OrderId is not null)
                throw new InvalidOperationException("Pedido vinculado.");
        }

        private void EnsureIsOrder()
        {
            if (OrderId is null)
                throw new InvalidOperationException("Sem pedido vinculado.");
        }

        private void EnsureIsLocked()
        {
            if (!StatusAccess.StatusIsLocked())
                throw new InvalidOperationException("Mesa precisa estar bloqueada.");
        }

        private void EnsuereUserHasLocked(UserId userId)
        {
            if (!StatusAccess.StatusIsLocked())
                throw new InvalidOperationException("Mesa não está bloqueada.");

            if (!StatusAccess.IsSameUser(userId))
                throw new InvalidOperationException("Úsuario não possui o bloqueio.");
        }
    }
}
