import axios from "axios"

const API_URL = "//http://localhost:7295/api";
const apiClient = axios.create({
    baseURL: API_URL,
    headers: {
        "Content-Type": "application/Json",
    },
});

//
export const getSubjectsByGrade = async (gradeId) => {
    const response = await apiClient.get(`/grades/${gradeId}/subjects`);
    return response.data;
}

export const getTeachersBySubject = async (subjectId) => {
    const response = await apiClient.get(`/subjects/${subjectId}/teachers`)
    return response.data;
}

export const saveReview = async () => {
    const response = await apiClient.post(`reviews`)
    return response.data;
}

export const getReviewsBySubject = async (subjectId) => {
    const response = await apiClient.get(`/subjects/${subjectId}/reviews`)
    return response.data;
}

export const getSubjectsByTeacher = async (teacherId) => {
    const response = await apiClient.get(`/teachers/${teacherId}/subjects`)
    return response.data;
}

export const getAllReviews = async () => {
    const response = await apiClient.get(`/reviews`)
    return response.data;
}

export const getReviewStats = async () => {
    const response = await apiClient.get(`/reviews/stats`)
    return response.data;
}

export const setFormAvailability = async (avaliability) => {
    const response = await apiClient.post(`/setFormAvailability/${avaliability}`)
    return response.data;
}