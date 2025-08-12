import axios from "axios"

const API_URL = "//http://localhost:7295/api";
const apiClient = axios.create({
    baseURL:API_URL,
    headers:{
        "Content-Type":"application/Json",
    },
});

export const getSubjectsByGrade = async (grade) =>{
    const response=await apiClient.get(`/getSubjectsByGrade/${grade}`);
    return response.data;
}

export const getTeachersBySubject = async (subject) => {
    const response = await apiClient.get(`/getTeachersBySubject/${subject}`)
    return response.data;
}

export const  saveReview = async (review) => {
    const response = await apiClient.post(`saveReview/${review}`)
    return response.data;
}

export const  getReviewsBySubject = async (subject) => {
    const response = await apiClient.get(`/getReviewsBySubject/${subject}`)
    return response.data;
}

export const  getSubjectsByTeacher = async (teacher) => {
    const response = await apiClient.get(`/getSubjectsByTeacher/${teacher}`)
    return response.data;
}

export const  getAllReviews = async () => {
    const response = await apiClient.get(`/getAllReviews`)
    return response.data;
}

export const  getReviewStats = async () => {
    const response = await apiClient.get(`/getReviewStats`)
    return response.data;
}

export const  setFormAvailability = async (avaliability) => {
    const response = await apiClient.post(`/setFormAvailability/${avaliability}`)
    return response.data;
}