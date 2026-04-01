using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.Auth.Results;

public sealed record LoginResult(string AccessToken, string RefreshToken, int ExpiredInSeconds);