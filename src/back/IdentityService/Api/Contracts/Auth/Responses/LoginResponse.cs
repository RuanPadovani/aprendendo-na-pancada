using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Contracts.Auth.Responses;

public sealed record LoginResponse(string AccessToken, int ExpiredInSeconds);
