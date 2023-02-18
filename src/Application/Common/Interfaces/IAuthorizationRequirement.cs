using CleanCRM.Application.Common.Models;
using MediatR;

namespace CleanCRM.Application.Common.Interfaces;

public interface IAuthorizationRequirement : IRequest<Result> { }
