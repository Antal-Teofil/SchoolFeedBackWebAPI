import { useQuery, useQueryClient, useMutation} from "@tanstack/react-query"
import {getSubjectsByGrade,getTeachersBySubject,saveReview,getReviewsBySubject,getSubjectsByTeacher,getReviewStats,setFormAvailability,getAllReviews} from "@/api/ReviewApi"

export const useReviews = () =>{
    const client=useQueryClient();
    const{
        data:subjectsByGrade,
        isLoading : isLoadingSubjectsByGrade,
        isError : isErrorSubjectsByGrade,
        error : errorSubjectsByGrade
    } = useQuery({
        queryKey: [`subjectsByGrade`,grade],
        queryFn:getSubjectsByGrade(grade)
    })
        
    const{
        data:teachersBySubject,
        isLoading : isLoadingTeachersBySubject,
        isError : isErrorTeachersBySubject,
        error : errorTeachersBySubject
    } = useQuery({
        queryKey: [`teachersBySubject`,subject],
        queryFn:getTeachersBySubject(subject)
    })

    const{
        data:reviewsBySubject,
        isLoading : isLoadingReviewsBySubject,
        isError : isErrorReviewsBySubject,
        error : errorReviewsBySubject
    } = useQuery({
        queryKey: [`reviews`,subject],
        queryFn:getReviewsBySubject(subject)
    })

    const{
        data:subjectsByTeacher,
        isLoading : isLoadingSubjectsByTeacher,
        isError : isErrorSubjectsByTeacher,
        error : errorReviewsSubjectsByTeacher
    } = useQuery({
        queryKey: [`reviews`,teacher],
        queryFn:getSubjectsByTeacher(teacher)
    })

    const {
        data: statsReviews,
        isLoading:isLoadingStatsReview,
        isError:isErrorStatsReview,
        error:errorStatsReview,
    } = useQuery({
        queryKey: ['statsReviews'],
        queryFn: getReviewStats,
    })

    const {
        data: allReviews,
        isLoading:isLoadingAllReviews,
        isError:isErrorAllReviews,
        error:errorAllReview,
    } = useQuery({
        queryKey: ['allReviews'],
        queryFn: getAllReviews,
    })

    const {mutate: saveReviewItem , isPending: isSavingReview } = useMutation({
        mutationFn: saveReview,
        onSuccess: (review) => {
            client.invalidateQueries({
                queryKey: ['reviews',review]
            });
        }
    })

     const {mutate: setFormAvailability , isPending: isSettingFormAvailability} = useMutation({
        mutationFn: setFormAvailability,
        onSuccess: (avaliability) => {
            client.invalidateQueries({
                queryKey: ['reviews',avaliability]
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
        setFormAvailability,
        isSettingFormAvailability
    }
}