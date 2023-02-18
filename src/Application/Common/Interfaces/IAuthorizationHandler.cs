using CleanCRM.Application.Common.Models;
using MediatR;

namespace CleanCRM.Application.Common.Interfaces;

public interface IAuthorizationHandler<TRequest> : IRequestHandler<TRequest, Result> where TRequest : IRequest<Result> { }
