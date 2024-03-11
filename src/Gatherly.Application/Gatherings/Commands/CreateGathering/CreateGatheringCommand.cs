using Gatherly.Domain.Entities;
using MediatR;

namespace Gatherly.Application.Gatherings.Commands.CreateGathering;

public record CreateGatheringCommand(
    Guid MemberId,
    GatheringType Type,
    DateTime ScheduledAtUtc,
    string Name,
    string? Location,
    int? MaximumNumberOfAttendees,
    int? InvitationsValidBeforeInHours
) : IRequest;