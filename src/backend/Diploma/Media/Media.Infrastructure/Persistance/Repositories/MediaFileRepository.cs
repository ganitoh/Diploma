using Common.Infrastructure;
using Media.Application.Persistance.Repositories;
using Media.Domain.Models;
using Media.Infrastructure.Persistance.Context;

namespace Media.Infrastructure.Persistance.Repositories;

public class MediaFileRepository : Repository<MediaFile, MediaDbContext>, IMediaFileRepository
{
    public MediaFileRepository(MediaDbContext dbContext) 
        : base(dbContext) { }
}