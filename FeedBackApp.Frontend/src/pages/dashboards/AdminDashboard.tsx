import { useState } from "react";
import { CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { toast } from "sonner";
import { useReviews } from "@/hooks/useReviews";
import { Console } from "console";

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

  // ==========================
  // Export (console log)
  // ==========================
  const exportExcel = () => {
    if (!questionnairesSummary) {
      toast.error("No questionnaire summary available to export.");
      return;
    }

    console.log("Questionnaire summary:", questionnairesSummary);

    if (isLoadingQuestionnairesSummary) {
      toast("Exporting results to Excel...");
    } else {
      toast.success("Export ready! Check console.");
    }
  };

  // ==========================
  // Send Questionnaires
  // ==========================
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
          console.log(endDate);
          console.log("Failed to create questionnaires.");
          toast.error("Failed to create questionnaires.");
        }
      }
    );
  };

  // ==========================
  // Delete Questionnaires
  // ==========================
  const deleteQuestionnaires = () => {
    // Globális törlés, ha az API engedi, különben végig kell menni a summary-n
    deleteQuestionnaire(undefined, {
      onSuccess: () => {
        console.log("Questionnaires deleted successfully!");
        toast.success("Questionnaires deleted successfully!");
      },
      onError: () => {
        console.log("Failed to delete questionnaires.");
        toast.error("Failed to delete questionnaires.");
      }
    });

    if (isDeletingQuestionnaire) {
      toast("Deleting questionnaires...");
    }
  };

  // ==========================
  // JSX
  // ==========================
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
        <Button onClick={sendQuestionnaires} disabled={isCreatingQuestionnaire}>
          Send Questionnaires to students!
        </Button>
        <Button onClick={deleteQuestionnaires} disabled={isDeletingQuestionnaire}>
          Delete Questionnaires
        </Button>
      </div>
    </main>
  );
}
