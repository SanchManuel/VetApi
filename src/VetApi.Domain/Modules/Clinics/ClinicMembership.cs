

namespace VetApi.Domain.Modules.Clinics
{
    public sealed class ClinicMembership
    {
        private ClinicMembership()
        {
        }

        public ClinicMembership(
            Guid clinicId,
            Guid userId,
            ClinicRole role,
            DateTime joinedAtUtc)
        {
            ClinicId = clinicId;
            UserId = userId;
            Role = role;
            JoinedAtUtc = joinedAtUtc;
            IsActive = true;
        }

        public Guid ClinicId { get; private set; }

        public Guid UserId { get; private set; }

        public ClinicRole Role { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime JoinedAtUtc { get; private set; }

        public VeterinaryClinic Clinic { get; private set; } = null!;
    }
}