import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

export default function HodDashboard() {
  return (
    <main className="container mx-auto px-6 py-10">
      <header className="mb-8">
        <h1 className="text-3xl font-bold">Management Dashboard</h1>
        <p className="text-muted-foreground">Aggregated school-wide results and trends.</p>
      </header>

      <section className="grid gap-6 md:grid-cols-3">
        <Card>
          <CardHeader>
            <CardTitle>School Average</CardTitle>
          </CardHeader>
          <CardContent className="text-3xl font-semibold">4.1</CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Subjects Above 4.5</CardTitle>
          </CardHeader>
          <CardContent className="text-3xl font-semibold">8</CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Responses</CardTitle>
          </CardHeader>
          <CardContent className="text-3xl font-semibold">1,246</CardContent>
        </Card>
      </section>

      <section className="mt-8 grid gap-6 md:grid-cols-2">
        <Card>
          <CardHeader>
            <CardTitle>Highlights</CardTitle>
          </CardHeader>
          <CardContent className="text-muted-foreground">
            <ul className="list-disc pl-5 space-y-2">
              <li>Positive trend in senior grades.</li>
              <li>Focus on engagement for grades 7–8.</li>
              <li>Anonymous by design. No identities stored.</li>
            </ul>
          </CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Publishing</CardTitle>
          </CardHeader>
          <CardContent className="text-muted-foreground">
            Authorized personnel can publish anonymized, aggregated results.
          </CardContent>
        </Card>
      </section>
    </main>
  );
}
