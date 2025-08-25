import { useState } from "react";
import { CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { toast } from "sonner";
import { useReviews } from "@/hooks/useReviews";

export default function AdminDashboard() {
  const [endDate, setEndDate] = useState<Date | undefined>();

  const {
    createQuestionnaires,
    isCreatingQuestionnaire,
    questionnairesSummary,
    isLoadingQuestionnairesSummary,
    deleteQuestionnaire,
    isDeletingQuestionnaire
  } = useReviews();

  const exportExcel = () => {
    if (!questionnairesSummary) {
      toast.error("No questionnaire summary available to export.");
      return;
    }
    console.log("Questionnaire summary:", questionnairesSummary);
    if (isLoadingQuestionnairesSummary) {
      toast("Exporting results to Excel...");
    }
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
        onSuccess: () => toast.success("Questionnaires created and sent!"),
        onError: () => toast.error("Failed to create questionnaires.")
      }
    );
  };


  const deleteQuestionnaires = () => {
    deleteQuestionnaire(undefined,
      {
        onSuccess: () => toast.success("Questionnaires deleted successfully!"),
        onError: () => toast.error("Failed to delete questionnaires.")
      }
    );
    if (isDeletingQuestionnaire) {
      toast("creating questionaries");
    };
  }

  return (

    <main className="container mx-auto px-6 py-10">
      <header className="mb-8">
        <h1 className="text-3xl font-bold">Admin Dashboard</h1>
        <p className="text-muted-foreground">Manage feedback windows, access, and exports.</p>
      </header>

      <CardContent>
        <input
          type="date"
          className="border rounded p-2 w-full"
          value={endDate ? endDate.toISOString().split("T")[0] : ""}
          onChange={(e) => setEndDate(new Date(e.target.value))}
        />
      </CardContent>


      <div className="mt-6 flex flex-row gap-4">
        <Button onClick={exportExcel}>Export evaluations</Button>
        <Button onClick={sendQuestionnaires}>Send Questionnaires to students!</Button>
        <Button onClick={deleteQuestionnaires}>Delete Questionnaires</Button>
      </div>
    </main>
  );
}
