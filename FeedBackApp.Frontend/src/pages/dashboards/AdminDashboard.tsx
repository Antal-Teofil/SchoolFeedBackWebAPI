import { useState } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Calendar } from "@/components/ui/calendar";
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { toast } from "sonner";
import { useReviews } from "@/hooks/useReviews";

const grades = Array.from({ length: 8 }).map((_, i) => 5 + i);

export default function AdminDashboard() {
  const [startDate, setStartDate] = useState<Date | undefined>(new Date());
  const [endDate, setEndDate] = useState<Date | undefined>();
  const [allowed, setAllowed] = useState<number[]>(grades);

  const {
    createQuestionnaires,
    isCreatingQuestionnaire,
    questionnairesSummary,
    isLoadingQuestionnairesSummary,
    deleteQuestionnaire,
    isDeletingQuestionnaire
  } = useReviews();

  const toggleGrade = (g: number) => {
    setAllowed((prev) => (prev.includes(g) ? prev.filter((x) => x !== g) : [...prev, g]));
  };

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
    createQuestionnaires(
      { startDate, endDate, allowedGrades: allowed },
      {
        onSuccess: () => toast.success("Questionnaires created and sent!"),
        onError: () => toast.error("Failed to create questionnaires.")
      }
    );
    if (isCreatingQuestionnaire) {
      toast("creating questionaries");
    };
  }

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

      <section className="grid gap-6 md:grid-cols-3">
        <Card>
          <CardHeader>
            <CardTitle>Start Date</CardTitle>
          </CardHeader>
          <CardContent>
            <Calendar mode="single" selected={startDate} onSelect={setStartDate} />
          </CardContent>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>End Date</CardTitle>
          </CardHeader>
          <CardContent>
            <Calendar mode="single" selected={endDate} onSelect={setEndDate} />
          </CardContent>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>Allowed Grades</CardTitle>
          </CardHeader>
          <CardContent className="grid grid-cols-2 gap-3">
            {grades.map((g) => (
              <label key={g} className="flex items-center gap-2">
                <Checkbox checked={allowed.includes(g)} onCheckedChange={() => toggleGrade(g)} />
                <span>{g}</span>
              </label>
            ))}
          </CardContent>
        </Card>
      </section>

      <div className="mt-6 flex flex-row gap-4">
        <Button onClick={exportExcel}>Export results to Excel</Button>
        <Button onClick={sendQuestionnaires}>Send Questionnaires to students!</Button>
        <Button onClick={deleteQuestionnaires}>Delete Questionnaires</Button>
      </div>
    </main>
  );
}
