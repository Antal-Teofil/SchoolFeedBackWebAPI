import { useQuery, useQueryClient, useMutation } from "@tanstack/react-query"
import { CreateQuestionnaires, GetQuestionnaireSummary, GetEvaluation, UpdateEvaluation, DeleteQuestionnaire, LoginWithGoogle, GetFormByEmail, StartQuestionnaire } from "@/api/ReviewApi"
import { useParams } from "react-router-dom";
import { StudentContext } from "@/models/StudentContext"

export const useReviews = (email?) => {
    const client = useQueryClient();
    const { questionnaireId, evaluationId } = useParams();

    const { mutate: createQuestionnaires, isPending: isCreatingQuestionnaire } = useMutation({
        mutationFn: (payload: { startDate: string; endDate: string }) => CreateQuestionnaires(payload),
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
        queryFn: () => GetQuestionnaireSummary(questionnaireId),
    })

    const {
        data: evaluation,
        isLoading: isLoadingEvaluation,
        isError: isErrorEvaluation,
        error: errorEvaluation
    } = useQuery({
        queryKey: [`evaluation`, evaluationId],
        queryFn: () => GetEvaluation(evaluationId),
        enabled: !!evaluationId
    })

    const {
        data: form,
        isLoading: isLoadingForm,
        isError: isErrorForm,
        error: errorForm
    } = useQuery<StudentContext>({
        queryKey: ['form', email],
        queryFn: () => GetFormByEmail(email!),
        enabled: !!email,
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
        mutationFn: (questionnaireId: string) => DeleteQuestionnaire(questionnaireId),
        onSuccess: (questionnaireId) => {
            client.invalidateQueries({
                queryKey: ['deletedQuestionnaire', questionnaireId],
            });
        }
    })

    const { mutate: startQuestionnaire, isPending: isStartingQuestionnaire } = useMutation({
        mutationFn: (questionnaireId: string) => StartQuestionnaire(questionnaireId),
        onSuccess: (questionnaireId) => {
            client.invalidateQueries({ queryKey: ['startedQuestionnaire', questionnaireId] });
        }
    })

     const { mutate: exportTeacherEvaluations, isPending: isExportingTeacher } = useMutation({
        mutationFn: (evaluationId: string) => GetEvaluation(evaluationId)
    });

    const { mutate: exportGlobalSummary, isPending: isExportingSummary } = useMutation({
        mutationFn: (questionnaireId: string) => GetQuestionnaireSummary(questionnaireId)
    });


    const { mutate: loginWithGoogle, isPending: isLoggingIn } = useMutation({
        mutationFn: (idToken: string) => LoginWithGoogle(idToken)
    });

    return {
        // Create
        createQuestionnaires, isCreatingQuestionnaire,
        // Summary
        questionnairesSummary, isLoadingQuestionnairesSummary, isErrorQuestionnairesSummary, errorQuestionnairesSummary,
        // Evaluation
        evaluation, isLoadingEvaluation, isErrorEvaluation, errorEvaluation,
        updateEvaluation, isUpdatingEvaluation,
        // Questionnaire actions
        deleteQuestionnaire, isDeletingQuestionnaire,
        startQuestionnaire, isStartingQuestionnaire,
        // Export
        exportTeacherEvaluations, isExportingTeacher,
        exportGlobalSummary, isExportingSummary,
        // Auth
        loginWithGoogle, isLoggingIn,
        // Forms
        form, isLoadingForm, isErrorForm, errorForm,
    }
}