﻿using Microsoft.EntityFrameworkCore;
using SchoolLocker.Core.Contracts;
using SchoolLocker.Core.Entities;
using SchoolLocker.Persistence.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SchoolLocker.Persistence
{
    public class UnitOfWork : Core.Contracts.IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed;
        private readonly OverlappingBookingValidation _overlapValidation;
        private readonly DuplicateLockerValidation _duplicateValidation;

        public UnitOfWork()
        {
            _dbContext = new ApplicationDbContext();
            BookingRepository = new BookingRepository(_dbContext);
            LockerRepository = new LockerRepository(_dbContext);
            PupilRepository = new PupilRepository(_dbContext);

            //TODO: Notwendige Services an die Validierungsattribute übergeben!
            _overlapValidation = new OverlappingBookingValidation();
            _duplicateValidation = new DuplicateLockerValidation();
        }

        public IBookingRepository BookingRepository { get; }
        public ILockerRepository LockerRepository { get; }
        public IPupilRepository PupilRepository { get; }


        /// <summary>
        /// Repository-übergreifendes Speichern der Änderungen
        /// </summary>
        public int SaveChanges()
        {
            //var entities = _dbContext.ChangeTracker.Entries()
            //    .Where(entity => entity.State == EntityState.Added
            //                     || entity.State == EntityState.Modified)
            //    .Select(e => e.Entity);
            //foreach (var entity in entities)
            //{
            //    ValidateEntity(entity);
            //}

            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Validierungen auf DbContext-Ebene
        /// </summary>
        /// <param name="entity"></param>
        private void ValidateEntity(object entity)
        {
            // Validierung der hinterlegten Validierungsattribute
            Validator.ValidateObject(entity, new ValidationContext(entity), true);

            ValidationResult result = null;

            switch (entity)
            {
                case Booking booking:
                    result = _overlapValidation.GetValidationResult(booking, new ValidationContext(booking));
                    break;
                case Locker locker:
                    result = _duplicateValidation.GetValidationResult(locker, new ValidationContext(locker));
                    break;
                default:
                    return;
            }

            if (result != null && result != ValidationResult.Success)
            {
                throw new ValidationException(result, _overlapValidation, entity);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void DeleteDatabase()
        {
            _dbContext.Database.EnsureDeleted();
        }

        public void MigrateDatabase()
        {
            _dbContext.Database.Migrate();
        }
    }
}
