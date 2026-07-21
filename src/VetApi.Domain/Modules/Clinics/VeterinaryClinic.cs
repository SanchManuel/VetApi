using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApi.Domain.Modules.Clinics
{
    public sealed class VeterinaryClinic
    {
        private VeterinaryClinic() { }
        public VeterinaryClinic(
            Guid id,
            string name,
            string timeZoneId,
            DateTime createdAtUtc)
        {
            Id = id;
            Name = name;
            TimeZoneId = timeZoneId;
            CreatedAtUtc = createdAtUtc;
            IsActive = true;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string TimeZoneId { get; private set; } = string.Empty;

        public bool IsActive { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }

        public ICollection<ClinicMembership> Memberships { get; private set; } = [];
    }
}