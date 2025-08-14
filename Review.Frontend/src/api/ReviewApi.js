import axios from "axios"

const API_URL = import.meta.env.VITE_API_BASE_URL;
const apiClient = axios.create({
    baseURL: API_URL,
    withCredentials: true,
    headers: {
        "Content-Type": "application/json",
    },
});

export const CreateQuestionnaires = async () => {
    const response = await apiClient.post(`/questionnaires`);
    return response.data;
}

export const GetQuestionnaireSummary = async (questionnaireId) => {
    const response = await apiClient.get(`/questionnaires/${questionnaireId}`)
    return response.data;
}

export const GetEvaluation = async (evaluationId) => {
    const response = await apiClient.get(`/evaluations/${evaluationId}`)
    return response.data;
}

export const UpdateEvaluation = async (evaluationId) => {
    const response = await apiClient.patch(`/evaluations/${evaluationId}`)
    return response.data;
}

export const DeleteQuestionnaire = async (questionnaireId) => {
    const response = await apiClient.delete(`/questionnaires/${questionnaireId}`)
    return response.data;
}

export const LoginWithGoogle = async (idToken) => {
    const { data } = await apiClient.post('/auth/google', { IdToken: idToken });
    return data;
};