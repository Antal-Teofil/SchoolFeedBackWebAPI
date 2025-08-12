import { useQuery, useQueryClient, useMutation } from "@tanstack/react-query"
import { getSubjectsByGrade, getTeachersBySubject, saveReview, getReviewsBySubject, getSubjectsByTeacher, getReviewStats, setFormAvailability, getAllReviews } from "@/api/ReviewApi"

export const useReviews = () => {
    const client = useQueryClient();
    const {
        data: subjectsByGrade,
        isLoading: isLoadingSubjectsByGrade,
        isError: isErrorSubjectsByGrade,
        error: errorSubjectsByGrade
    } = useQuery({
        queryKey: [`subjectsByGrade`, gradeId],
        queryFn: getSubjectsByGrade(gradeId)
    })

    const {
        data: teachersBySubject,
        isLoading: isLoadingTeachersBySubject,
        isError: isErrorTeachersBySubject,
        error: errorTeachersBySubject
    } = useQuery({
        queryKey: [`teachersBySubject`, subjectId],
        queryFn: getTeachersBySubject(subjectId)
    })

    const {
        data: reviewsBySubject,
        isLoading: isLoadingReviewsBySubject,
        isError: isErrorReviewsBySubject,
        error: errorReviewsBySubject
    } = useQuery({
        queryKey: [`reviews`, subjectId],
        queryFn: getReviewsBySubject(subjectId)
    })

    const {
        data: subjectsByTeacher,
        isLoading: isLoadingSubjectsByTeacher,
        isError: isErrorSubjectsByTeacher,
        error: errorReviewsSubjectsByTeacher
    } = useQuery({
        queryKey: [`reviews`, teacherId],
        queryFn: getSubjectsByTeacher(teacherId)
    })

    const {
        data: statsReviews,
        isLoading: isLoadingStatsReview,
        isError: isErrorStatsReview,
        error: errorStatsReview,
    } = useQuery({
        queryKey: ['statsReviews'],
        queryFn: getReviewStats,
    })

    const {
        data: allReviews,
        isLoading: isLoadingAllReviews,
        isError: isErrorAllReviews,
        error: errorAllReview,
    } = useQuery({
        queryKey: ['allReviews'],
        queryFn: getAllReviews,
    })

    const { mutate: saveReview, isPending: isSavingReview } = useMutation({
        mutationFn: saveReview,
        onSuccess: () => {
            client.invalidateQueries({
                queryKey: ['reviews']
            });
        }
    })

    return {
        subjectsByGrade,
        isLoadingSubjectsByGrade,
        isErrorSubjectsByGrade,
        errorSubjectsByGrade,
        teachersBySubject,
        isLoadingTeachersBySubject,
        isErrorTeachersBySubject,
        errorTeachersBySubject,
        reviewsBySubject,
        isLoadingReviewsBySubject,
        isErrorReviewsBySubject,
        errorReviewsBySubject,
        subjectsByTeacher,
        isLoadingSubjectsByTeacher,
        isErrorSubjectsByTeacher,
        errorReviewsSubjectsByTeacher,
        statsReviews,
        isLoadingStatsReview,
        isErrorStatsReview,
        errorStatsReview,
        allReviews,
        isLoadingAllReviews,
        isErrorAllReviews,
        errorAllReview,
        saveReviewItem,
        isSavingReview,
    }
}