using AlgoHub.API.Models;
using AlgoHub.BLL.Interfaces;
using AlgoHub.DAL;
using AlgoHub.DAL.Entities;
using AutoMapper;

namespace AlgoHub.BLL.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int?> AddLessonComment(LessonCommentCreateModel comment)
    {
        var newComment = _mapper.Map<LessonComment>(comment);
        newComment.Author = new User() { UserId = comment.AuthorId };

        return await _unitOfWork.CommentRepository.AddLessonComment(newComment);
    }

    public async Task<int?> AddProblemComment(ProblemCommentCreateModel comment)
    {
        var newComment = _mapper.Map<ProblemComment>(comment);
        newComment.Author = new User() { UserId = comment.AuthorId };

        return await _unitOfWork.CommentRepository.AddProblemComment(newComment);
    }

    public async Task<int?> AddSolveComment(SolveCommentCreateModel comment)
    {
        var newComment = _mapper.Map<SolveComment>(comment);
        newComment.Author = new User() { UserId = comment.AuthorId };

        return await _unitOfWork.CommentRepository.AddSolveComment(newComment);
    }

    public async Task<LessonCommentModel[]?> GetLessonComments(int lessonId)
    {
        LessonComment[]? lessonComments = await _unitOfWork.CommentRepository.GetLessonComments(lessonId);

        if(lessonComments == null)
        {
            return null;
        }

        return GenerateLessonCommentTree(lessonComments);
    }

    private LessonCommentModel[]? GenerateLessonCommentTree(LessonComment[] comments, int? rootCommentId = null)
    {
        LessonCommentModel[] roots = comments.Where(c => c.ParentCommentId == rootCommentId).Select(_mapper.Map<LessonCommentModel>).ToArray();

        if (roots.Length == 0)
        {
            return rootCommentId == null ? roots : null;
        }

        foreach (var root in roots)
        {
            root.Replies = GenerateLessonCommentTree(comments, root.LessonCommentId ?? -1);
        }

        return roots;
    }

    public async Task<ProblemCommentModel[]?> GetProblemComments(int problemId)
    {
        ProblemComment[]? problemComments = await _unitOfWork.CommentRepository.GetProblemComments(problemId);

        if (problemComments == null)
        {
            return null;
        }

        return GenerateProblemCommentTree(problemComments);
    }

    private ProblemCommentModel[]? GenerateProblemCommentTree(ProblemComment[] comments, int? rootCommentId = null)
    {
        ProblemCommentModel[] roots = comments.Where(c => c.ParentCommentId == rootCommentId).Select(_mapper.Map<ProblemCommentModel>).ToArray();

        if (roots.Length == 0)
        {
            return rootCommentId == null ? roots : null;
        }

        foreach (var root in roots)
        {
            root.Replies = GenerateProblemCommentTree(comments, root.ProblemCommentId ?? -1);
        }

        return roots;
    }

    public async Task<SolveCommentModel[]?> GetSolveComments(int solveId)
    {
        SolveComment[]? solveComments = await _unitOfWork.CommentRepository.GetSolveComments(solveId);

        if (solveComments == null)
        {
            return null;
        }

        return GenerateSolveCommentTree(solveComments);
    }

    private SolveCommentModel[]? GenerateSolveCommentTree(SolveComment[] comments, int? rootCommentId = null)
    {
        SolveCommentModel[] roots = comments.Where(c => c.ParentCommentId == rootCommentId).Select(_mapper.Map<SolveCommentModel>).ToArray();

        if (roots.Length == 0)
        {
            return rootCommentId == null ? roots : null;
        }

        foreach (var root in roots)
        {
            root.Replies = GenerateSolveCommentTree(comments, root.SolveCommentId ?? -1);
        }

        return roots;
    }
}