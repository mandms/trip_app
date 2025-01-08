﻿using Application.Dto.Coordinates;
using Application.Dto.Image;
using Application.Dto.Moment;
using Domain.Entities;

namespace Application.Mappers
{
    public class MomentMapper
    {
        public static MomentDto MomentToMomentDto(Moment moment)
        {
            var coordinates = moment.Coordinates.Coordinate.CoordinateValue;

            return new MomentDto
            {
                Id = moment.Id,
                Description = moment.Description,
                Coordinates = new CoordinatesDto
                {
                    Latitude = coordinates.Y,
                    Longitude = coordinates.X,
                },
                CreatedAt = moment.CreatedAt.ToString("dd/MM/yyyy"),
                User = UserMapper.UserAuthor(moment.User),
                Status = moment.Status,
                Images = ImageMapper.ImageMomentToString(moment.Images)
            };
        }

        public static Moment ToMoment(CreateMomentDto createMomentDto)
        {
            List<ImageMoment> images = GetImages(createMomentDto.Images);
            return new Moment
            {
                Coordinates = CoordinatesDto.CreatePoint(createMomentDto.Coordinates),
                Description = createMomentDto.Description,
                Status = createMomentDto.Status,
                UserId = createMomentDto.UserId,
                Images = images
            };
        }

        public static List<ImageMoment> GetImages(List<CreateImageDto>? imageDtos)
        {
            List<ImageMoment> images = new();
            if ((imageDtos != null) && (imageDtos.Count() > 0))
            {
                images = imageDtos.Select(image => new ImageMoment { Image = image.Path }).ToList();
            }
            return images;
        }

        public static void UpdateMoment(Moment moment, UpdateMomentDto updateMomentDto)
        {
            moment.Coordinates = CoordinatesDto.CreatePoint(updateMomentDto.Coordinates);
            moment.Description = updateMomentDto.Description;
            moment.Status = updateMomentDto.Status;
        }
    }
}
