


global using Files.BLL.Services;
global using Files.Domain.Services;
global using Microsoft.Extensions.FileProviders;
global using Files.Domain.Models;
global using Grpc.Core;
global using Files.gRPC.v1;
global using Files.gRPC.Mappers;
global using Files.gRPC.Compression;
global using Files.gRPC.Interceptors;
global using Grpc.Net.Compression;
global using System.IO.Compression;
global using Files.Domain.Repositories;
global using Files.DAL.Repositories;
global using Files.DAL.Database;
global using Microsoft.EntityFrameworkCore;
global using Files.gRPC.Middleware;