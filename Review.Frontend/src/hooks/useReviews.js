import { useQuery, useQueryClient, useMutation } from "@tanstack/react-query"
import { CreateQuestionnaires, GetQuestionnaireSummary, GetEvaluation, UpdateEvaluation, DeleteQuestionnaire, LoginWithGoogle } from "@/api/ReviewApi"
import { useParams } from "react-router-dom";

export const useReviews = () => {
    const client = useQueryClient();
    const { questionnaireId, evaluationId } = useParams();

    const { mutate: createQuestionnaires, isPending: isCreatingQuestionnaire } = useMutation({
        mutationFn: CreateQuestionnaires,
        onSuccess: () => {
            client.invalidateQueries({
                queryKey: ['questionnaires']
            });
        }
    })

    const {
        data: questionnairesSummary,
        isLoading: isLoadingQuestionnairesSummary,
        isError: isErrorQuestionnairesSummary,
        error: errorQuestionnairesSummary
    } = useQuery({
        queryKey: [`questionnairesSummary`, questionnaireId],
        queryFn: GetQuestionnaireSummary(questionnaireId)
    })

    const {
        data: evaluation,
        isLoading: isLoadingEvaluation,
        isError: isErrorEvaluation,
        error: errorEvaluation
    } = useQuery({
        queryKey: [`evaluation`, evaluationId],
        queryFn: GetEvaluation(evaluationId)
    })


    const { mutate: updateEvaluation, isPending: isUpdatingEvaluation } = useMutation({
        mutationFn: UpdateEvaluation,
        onSuccess: (evaluationId) => {
            client.invalidateQueries({
                queryKey: ['updatedEvaluation', evaluationId]
            });
        }
    })

    const { mutate: deleteQuestionnaire, isPending: isDeletingQuestionnaire } = useMutation({
        mutationFn: DeleteQuestionnaire,
        onSuccess: (questionnaireId) => {
            client.invalidateQueries({
                queryKey: ['deletedQuestionnaire', questionnaireId]
            });
        }
    })

const { mutate: loginWithGoogle, isPending: isLoggingIn, error: loginError } = useMutation({
    mutationFn: LoginWithGoogle,
    onSuccess: (data) => {
        client.invalidateQueries({ queryKey: ['user'] })
    }
});


    return {
        createQuestionnaires,
        isCreatingQuestionnaire,
        questionnairesSummary,
        isLoadingQuestionnairesSummary,
        isErrorQuestionnairesSummary,
        errorQuestionnairesSummary,
        evaluation,
        isLoadingEvaluation,
        isErrorEvaluation,
        errorEvaluation,
        updateEvaluation,
        isUpdatingEvaluation,
        deleteQuestionnaire,
        isDeletingQuestionnaire,
        loginWithGoogle,
        isLoggingIn,
        loginError,
    }
}