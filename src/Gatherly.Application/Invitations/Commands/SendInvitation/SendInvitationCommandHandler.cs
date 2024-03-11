﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using MediatR;

namespace Gatherly.Application.Invitations.Commands.SendInvitation;

public sealed class SendInvitationCommandHandler : IRequestHandler<SendInvitationCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public SendInvitationCommandHandler(IMemberRepository memberRepository, IGatheringRepository gatheringRepository, IInvitationRepository invitationRepository, IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _memberRepository = memberRepository;
        _gatheringRepository = gatheringRepository;
        _invitationRepository = invitationRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }


    public async Task<Unit> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if (member is null || gathering is null)
        {
            return Unit.Value;
        }

        if (gathering.Creator.Id == member.Id)
        {
            throw new Exception("Can t send invitation to the gathering creator");
        }

        if (gathering.ScheduledAtUtc < DateTime.UtcNow)
        {
            throw new Exception("Can t send invitation for gathering in the past");
        }

        gathering.SendInvitation(member);

        gathering.Invitations.Add(invitation);

        _invitationRepository.Add(invitation);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _emailService.SendInvitationSentEmailAsync(member,gathering,cancellationToken);

        return Unit.Value;
    }
}