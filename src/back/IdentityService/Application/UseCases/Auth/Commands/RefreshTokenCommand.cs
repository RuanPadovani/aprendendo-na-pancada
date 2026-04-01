using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.Auth.Commands;

public sealed record RefreshTokenCommand(string RefreshToken);
