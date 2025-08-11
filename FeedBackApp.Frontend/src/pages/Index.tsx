import { Link } from "react-router-dom";
import { Helmet } from "react-helmet-async";
import Header from "@/components/Header";
import { Button } from "@/components/ui/button";

const Index = () => {
  return (
    <>
      <Helmet>
        <title>Class Feedback Portal</title>
        <meta name="description" content="Students submit anonymous feedback. Teachers view insights and export Excel statistics." />
        <link rel="canonical" href={typeof window !== 'undefined' ? window.location.href : '/'} />
      </Helmet>
      <Header role="guest" />
      <main className="min-h-[calc(100vh-4rem)] flex items-center">
        <section className="container mx-auto px-4 grid gap-8 md:grid-cols-2 items-center">
          <div className="space-y-6">
            <h1 className="text-4xl md:text-5xl font-bold leading-tight">A simple, beautiful way to collect class feedback</h1>
            <p className="text-lg text-muted-foreground max-w-prose">
              Students can easily submit anonymous feedback by subject and teacher. Teachers see all feedback and export global statistics to Excel.
            </p>
            <div className="flex flex-wrap gap-3">
              <Link to="/student"><Button variant="hero" size="lg">I am a Student</Button></Link>
              <Link to="/teacher"><Button variant="secondary" size="lg">I am a Teacher</Button></Link>
            </div>
          </div>
          <div className="rounded-xl border bg-gradient-primary p-8 text-primary-foreground card-elevated">
            <div className="space-y-4">
              <h2 className="text-2xl font-semibold">How it works</h2>
              <ol className="space-y-2 text-base">
                <li>1. Students select a subject and teacher.</li>
                <li>2. They rate and write feedback (autosaves drafts).</li>
                <li>3. Teachers view all anonymous feedback.</li>
                <li>4. Export Excel with global statistics.</li>
              </ol>
            </div>
          </div>
        </section>
      </main>
    </>
  );
};

export default Index;
