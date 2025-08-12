import { useState } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Calendar } from "@/components/ui/calendar";
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { toast } from "sonner";

const grades = Array.from({ length: 8 }).map((_, i) => 5 + i);

export default function AdminDashboard() {
  const [startDate, setStartDate] = useState<Date | undefined>(new Date());
  const [endDate, setEndDate] = useState<Date | undefined>();
  const [allowed, setAllowed] = useState<number[]>(grades);

  const toggleGrade = (g: number) => {
    setAllowed((prev) => (prev.includes(g) ? prev.filter((x) => x !== g) : [...prev, g]));
  };

  const exportExcel = () => {
    toast("Export will be available after backend setup (Excel).");
  };

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

      <div className="mt-6">
        <Button onClick={exportExcel}>Export results to Excel</Button>
      </div>
    </main>
  );
}
