using Common.Application;
using MediatR;

namespace Media.Application.CQRS.MediaFiles.Commands;

public class CreateMediaFileCommand : ICommand<Unit> { }