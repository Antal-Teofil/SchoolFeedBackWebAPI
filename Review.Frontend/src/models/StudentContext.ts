export type EvaluationResponses ={
    [key: `q${number}`]: string | string[];
}

export type Evaluation = {
    id: string;
    subject: string;
    teacher: string;
    status: string;
    responses: EvaluationResponses;
}

export type StudentContext = {
    grade: string;
    subjects: string[];
    teachers: string[];
    evaluations: Evaluation[];
}