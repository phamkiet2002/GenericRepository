//using AutoMapper;
//using Microsoft.AspNetCore.Http.HttpResults;
//using WebAPI_Tutorial.Model;
//using Microsoft.AspNetCore.Http;
//using WebTutorial.Dtos.CommentDto;
//using WebTutorial.Dtos.Stock;

//namespace WebTutorial.Mapper
//{
//    public class AutoMapperConfig : Profile
//    {
//        public AutoMapperConfig()
//        {
//            //CreateMap<CommentEntity, CommentDtos>();
//            //CreateMap<CommentDtos, CommentEntity>();

//            //CreateMap<CommentDtos, CommentEntity>().ForMember(c =>  c.Stock.Id, opt => opt.MapFrom(x => x.StockId));
//            //CreateMap<CommentDtos, CommentEntity>().ForPath(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock.Id));

//            //CreateMap<CommentDtos, CommentEntity>().IncludeMembers(c => c.Stock);

//            //CreateMap<CommentDtos, CommentEntity>().ReverseMap();

//            CreateMap<CommentEntity, CommentDtos>().ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock)).ReverseMap();
//            //CreateMap<CommentEntity, CommentDtos>()
//            //        .ForMember(dest => dest.Stock,
//            //        opt => opt.MapFrom(src =>
//            //        new StockDto
//            //        {
//            //            Id = src.Stock.Id,
//            //            Company = src.Stock.Company,
//            //            Industry = src.Stock.Industry,
//            //            LastDiv = src.Stock.LastDiv,
//            //            Sybol = src.Stock.Sybol,
//            //            Purchase = src.Stock.Purchase,
//            //            MarketCap = src.Stock.MarketCap,
//            //        }));


//            CreateMap<List<CommentEntity>, List<CommentDtos>>().ConvertUsing(entities => entities.Select(entity => new CommentDtos
//            {
//                Id = entity.Id,
//                Content = entity.Content,
//                Stock = new StockDto
//                {
//                    Id = entity.Stock.Id,
//                    Company = entity.Stock.Company,
//                    Industry = entity.Stock.Industry,
//                    LastDiv = entity.Stock.LastDiv,
//                    Sybol = entity.Stock.Sybol,
//                    Purchase = entity.Stock.Purchase,
//                    MarketCap = entity.Stock.MarketCap
//                }}).ToList());

//            CreateMap<StockDto, StockEntity>().ReverseMap();
//        }
//    }
//}
