import { useState } from "react";
import { CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { toast } from "sonner";
import { useReviews } from "@/hooks/useReviews";
import { useAuthStore } from "@/stores/useAuthStore";
import { Navigate } from "react-router-dom";

export default function AdminDashboard() {
  const user = useAuthStore((s) => s.user);
  console.log(user);
  if (!user) return <Navigate to="/" replace />;
  if (user.role !== "Admin") return <Navigate to="/no-access" replace />

  const [endDate, setEndDate] = useState<Date | undefined>();
  const [selectedQuestionnaireId, setSelectedQuestionnaireId] = useState<string | undefined>();

  const {
    createQuestionnaires,
    isCreatingQuestionnaire,
    questionnairesSummary,
    isLoadingQuestionnairesSummary,
    deleteQuestionnaire,
    isDeletingQuestionnaire,
    exportQuestionnaire,
    isExporting
  } = useReviews();

  const mockQuestionnaires = [
    { id: "q1", title: "Math Evaluation Spring 2025" },
    { id: "q2", title: "Physics Midterm Survey" },
    { id: "q3", title: "Chemistry Final Feedback" }
  ];

  const displayedQuestionnaires = questionnairesSummary || mockQuestionnaires;

  const exportExcel = () => {
    if (!selectedQuestionnaireId) {
      toast.error("Select a questionnaire first!");
      return;
    }

    exportQuestionnaire(selectedQuestionnaireId, {
      onSuccess: () => {
        console.log("Exported questionnaire:", selectedQuestionnaireId);
        toast.success("Exported questionnaire!");
      },
      onError: () => {
        console.log("Failed to export questionnaire:", selectedQuestionnaireId);
        toast.error("Failed to export questionnaire.");
      }
    });
  };

  const sendQuestionnaires = () => {
    if (!endDate) {
      toast.error("Please set an end date.");
      return;
    }
    createQuestionnaires(
      {
        startDate: new Date().toISOString(),
        endDate: endDate.toISOString()
      },
      {
        onSuccess: () => {
          console.log("Questionnaires created and sent!");
          toast.success("Questionnaires created and sent!");
        },
        onError: () => {
          console.log("Failed to create questionnaires.");
          toast.error("Failed to create questionnaires.");
        }
      }
    );
  };

  const deleteSelectedQuestionnaire = () => {
    if (!selectedQuestionnaireId) {
      toast.error("Select a questionnaire first!");
      return;
    }
    deleteQuestionnaire(selectedQuestionnaireId, {
      onSuccess: () => {
        console.log("Deleted questionnaire:", selectedQuestionnaireId);
        toast.success("Deleted questionnaire!");
      },
      onError: () => {
        console.log("Failed to delete questionnaire:", selectedQuestionnaireId);
        toast.error("Failed to delete questionnaire.");
      }
    });
  };

  return (
    <main className="container mx-auto px-6 py-10">
      <header className="mb-8">
        <h1 className="text-3xl font-bold">Admin Dashboard</h1>
        <p className="text-muted-foreground">Manage feedback windows, access, and exports.</p>
      </header>

      <CardContent>
        <input
          type="date"
          className="border rounded p-2 w-full mb-4"
          value={endDate ? endDate.toISOString().split("T")[0] : ""}
          onChange={(e) => setEndDate(new Date(e.target.value))}
        /></CardContent>
      <div className="mt-6 flex flex-row gap-4">
        <Button onClick={sendQuestionnaires} disabled={isCreatingQuestionnaire || !endDate}>Send Questionnaires</Button>
      </div>
      <br />
      <CardContent>
        <select
          className="border rounded p-2 w-full"
          value={selectedQuestionnaireId}
          onChange={(e) => setSelectedQuestionnaireId(e.target.value)}
        >
          <option value="">-- Select a questionnaire --</option>
          {displayedQuestionnaires?.map((q: any) => (
            <option key={q.id} value={q.id}>
              {q.title || q.id}
            </option>
          ))}
        </select>
      </CardContent>

      <div className="mt-6 flex flex-row gap-4">
        <Button onClick={exportExcel} disabled={!selectedQuestionnaireId || isExporting}>Export evaluations</Button>
        <Button onClick={deleteSelectedQuestionnaire} disabled={!selectedQuestionnaireId || isDeletingQuestionnaire}>Delete Selected Questionnaire</Button>
      </div>
    </main>
  );
}
