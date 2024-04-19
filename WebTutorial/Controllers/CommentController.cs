using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTutorial.Mapper;
using WebTutorial.Repository.Comment;
using WebAPI_Tutorial.Model;
using WebTutorial.Dtos.CommentDto;
using WebTutorial.Repository.Stock;
using WebTutorial.Dtos.Stock;
using WebTutorial.Helper;

namespace WebTutorial.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository CommentRepository;
        private readonly IStockRepository StockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository) 
        {
            CommentRepository = commentRepository;
            StockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await CommentRepository.GetAll();
            var commentDto = comments.Select(c => c.toCommentDto());
            return Ok(commentDto) ;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cmt = await CommentRepository.GettById(id);
            if (cmt == null) 
                return NotFound();
            return Ok(cmt.toCommentDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute]  int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cmt = await CommentRepository.Delete(id);
            if (cmt == null)
                return NotFound();
            return Content("Xóa Thành công");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDtos cmtDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cmtModel = cmtDtos.ToCommentFromCreate();
            await CommentRepository.Create(cmtModel);
            return Ok("tao thanh cong " + cmtModel);
        }

        //thêm cmt vào sản phẩm
        [HttpPost("{StockId}")]
        public async Task<IActionResult> CreateCommentSanpham([FromRoute] int StockId, CreateCommentSanphamDtos cmtDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await StockRepository.StockExists(StockId))
                return BadRequest();
            var cmtModel = cmtDtos.ToCommentFromCreateSanPham(StockId);
            await CommentRepository.Create(cmtModel);
            return CreatedAtAction(nameof(GetById), new { id = cmtModel.Id }, cmtModel.toCommentDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDtos commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cmt = await CommentRepository.Update(id, commentDto.ToCommentFromUpdate());
            if (cmt == null)
                return NotFound();
            return Ok(cmt.toCommentDto());
        }
    }
}
