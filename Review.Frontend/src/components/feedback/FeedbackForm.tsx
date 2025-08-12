import { useMemo, useState } from "react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import { Checkbox } from "@/components/ui/checkbox";
import { Input } from "@/components/ui/input";
import { toast } from "sonner";

interface FeedbackFormProps {
  subjects: { id: string; name: string }[];
  teachers: { id: string; name: string }[];
}

const grades = Array.from({ length: 8 }).map((_, i) => String(5 + i));

export function FeedbackForm({ subjects, teachers }: FeedbackFormProps) {
  // Alap azonosítók
  const [grade, setGrade] = useState<string>("");
  const [subjectId, setSubjectId] = useState<string>("");
  const [teacherId, setTeacherId] = useState<string>("");

  // Likert 1–5: Q1–Q12, Q13 (ütem), Q14–Q17
  const [q1, setQ1] = useState("");
  const [q2, setQ2] = useState("");
  const [q3, setQ3] = useState("");
  const [q4, setQ4] = useState("");
  const [q5, setQ5] = useState("");
  const [q6, setQ6] = useState("");
  const [q7, setQ7] = useState("");
  const [q8, setQ8] = useState("");
  const [q9, setQ9] = useState("");
  const [q10, setQ10] = useState("");
  const [q11, setQ11] = useState("");
  const [q12, setQ12] = useState("");
  const [q13, setQ13] = useState("");
  const [q14, setQ14] = useState("");
  const [q15, setQ15] = useState("");
  const [q16, setQ16] = useState("");
  const [q17, setQ17] = useState("");

  // Iskolán kívüli oktatás
  const [q18, setQ18] = useState("");
  const [q18Other, setQ18Other] = useState("");
  const [q19, setQ19] = useState("");
  const [q20, setQ20] = useState<string[]>([]); // többválasztós
  const [q21, setQ21] = useState<string[]>([]); // többválasztós

  // Szöveges
  const [q22, setQ22] = useState("");
  const [q23, setQ23] = useState("");

  // Jelenlét / elmaradt órák
  const [q24, setQ24] = useState("");
  const [q25, setQ25] = useState("");
  const [q26, setQ26] = useState("");

  const likertValues = ["1", "2", "3", "4", "5"];

  const isAttendingOutside = useMemo(
    () => q19 === "maganorak" || q19 === "csoportos",
    [q19]
  );

  const onSaveDraft = () => {
    toast("Piszkozat helyben elmentve. Backend csatlakoztatásával lesz tartós.");
  };

  const onSubmit = () => {
    if (!grade || !subjectId || !teacherId) {
      toast("Kérjük, válaszd ki az évfolyamot, tantárgyat és tanárt.");
      return;
    }

    const likerts = [
      q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, q11, q12, q13, q14, q15, q16, q17,
    ];
    if (likerts.some((v) => !v)) {
      toast("Kérjük, töltsd ki az osztálytermi tevékenység minden kérdését (1–17).");
      return;
    }

    if (!q18) {
      toast("Kérjük, válaszolj a 18. kérdésre.");
      return;
    }
    if (q18 === "egyeb" && !q18Other.trim()) {
      toast("Kérjük, töltsd ki a 18. kérdés 'egyéb' mezőjét.");
      return;
    }

    if (!q19) {
      toast("Kérjük, válaszolj a 19. kérdésre.");
      return;
    }
    if (isAttendingOutside && q20.length === 0) {
      toast("Kérjük, jelöld meg legalább egy okot a 20. kérdésnél.");
      return;
    }

    if (q21.length === 0) {
      toast("Kérjük, válassz legalább egy lehetőséget a 21. kérdésnél.");
      return;
    }

    if (!q24 || !q25 || !q26) {
      toast("Kérjük, töltsd ki a jelenlétre és elmaradt tanórákra vonatkozó kérdéseket (24–26).");
      return;
    }

    toast("Küldésre kész. Supabase engedélyezésével anonim módon tudjuk tárolni.");
  };

  const toggleMulti = (value: string, setFn: (updater: (prev: string[]) => string[]) => void) => {
    setFn((prev) => (prev.includes(value) ? prev.filter((v) => v !== value) : [...prev, value]));
  };

  return (
    <Card>
      <CardHeader>
        <CardTitle>Anonim visszajelzés beküldése</CardTitle>
      </CardHeader>
      <CardContent className="space-y-8">
        {/* Alap kiválasztások */}
        <section className="grid gap-4 md:grid-cols-3">
          <div className="space-y-2">
            <Label htmlFor="grade">Évfolyam</Label>
            <Select value={grade} onValueChange={setGrade}>
              <SelectTrigger id="grade">
                <SelectValue placeholder="Válassz évfolyamot (5–12)" />
              </SelectTrigger>
              <SelectContent>
                {grades.map((g) => (
                  <SelectItem key={g} value={g}>{g}</SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>
          <div className="space-y-2">
            <Label htmlFor="subject">Tantárgy</Label>
            <Select value={subjectId} onValueChange={setSubjectId}>
              <SelectTrigger id="subject">
                <SelectValue placeholder="Válassz tantárgyat" />
              </SelectTrigger>
              <SelectContent>
                {subjects.map((s) => (
                  <SelectItem key={s.id} value={s.id}>{s.name}</SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>
          <div className="space-y-2">
            <Label htmlFor="teacher">Tanár</Label>
            <Select value={teacherId} onValueChange={setTeacherId}>
              <SelectTrigger id="teacher">
                <SelectValue placeholder="Válassz tanárt" />
              </SelectTrigger>
              <SelectContent>
                {teachers.map((t) => (
                  <SelectItem key={t.id} value={t.id}>{t.name}</SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>
        </section>

        {/* OSZTÁLYTERMI TEVÉKENYSÉG */}
        <section className="space-y-6">
          <header>
            <h2 className="text-xl font-semibold">Osztálytermi tevékenység</h2>
            <p className="text-sm text-muted-foreground">1 = egyáltalán nem értek egyet, 5 = teljes mértékben egyetértek</p>
          </header>

          {/* Helper to render 1–5 radio rows */}
          <div className="space-y-5">
            <div className="space-y-2">
              <Label>1) A Tanár érthetően magyarázza a tananyagot.</Label>
              <RadioGroup value={q1} onValueChange={setQ1} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q1-${v}`} value={v} />
                    <Label htmlFor={`q1-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>2) A Tanár olyan magyarázatokat ad, amelyek segítenek a hatékony tanulásban.</Label>
              <RadioGroup value={q2} onValueChange={setQ2} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q2-${v}`} value={v} />
                    <Label htmlFor={`q2-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>3) A tanórai feladatok érdekesek.</Label>
              <RadioGroup value={q3} onValueChange={setQ3} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q3-${v}`} value={v} />
                    <Label htmlFor={`q3-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>4) A Tanár bátorítja és bevonja a teljes osztályközösséget a tanórák tevékenységébe.</Label>
              <RadioGroup value={q4} onValueChange={setQ4} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q4-${v}`} value={v} />
                    <Label htmlFor={`q4-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>5) A Tanár szorgalmazza a diákok közötti együttműködést.</Label>
              <RadioGroup value={q5} onValueChange={setQ5} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q5-${v}`} value={v} />
                    <Label htmlFor={`q5-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>6) A Tanár motivál, hogy ezeken az órákon részt vegyek.</Label>
              <RadioGroup value={q6} onValueChange={setQ6} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q6-${v}`} value={v} />
                    <Label htmlFor={`q6-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>7) A Tanár digitális eszközöket használ a tanításhoz.</Label>
              <RadioGroup value={q7} onValueChange={setQ7} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q7-${v}`} value={v} />
                    <Label htmlFor={`q7-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>8) Kielégítő tájékoztatást kapok a képességeim felmérésének módjairól, illetve ismereteim felmérésének kritériumairól.</Label>
              <RadioGroup value={q8} onValueChange={setQ8} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q8-${v}`} value={v} />
                    <Label htmlFor={`q8-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>9) A Tanár támogatja a tanulók közti versenyszellemet.</Label>
              <RadioGroup value={q9} onValueChange={setQ9} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q9-${v}`} value={v} />
                    <Label htmlFor={`q9-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>10) A tanulókat bátorítja gondolataik kifejezésére és a véleményalkotásra.</Label>
              <RadioGroup value={q10} onValueChange={setQ10} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q10-${v}`} value={v} />
                    <Label htmlFor={`q10-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>11) Az órákon kellemes a hangulat.</Label>
              <RadioGroup value={q11} onValueChange={setQ11} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q11-${v}`} value={v} />
                    <Label htmlFor={`q11-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>12) Biztonságban és komfortosan érzem magam az órákon.</Label>
              <RadioGroup value={q12} onValueChange={setQ12} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q12-${v}`} value={v} />
                    <Label htmlFor={`q12-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>13) A tananyag elsajátításának üteme számomra:</Label>
              <RadioGroup value={q13} onValueChange={setQ13} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q13-${v}`} value={v} />
                    <Label htmlFor={`q13-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
              <p className="text-xs text-muted-foreground">1 = nagyon lassú, 3 = megfelelő, 5 = nagyon gyors</p>
            </div>

            <div className="space-y-2">
              <Label>14) A Tanárt érdekli, hogy én jól érezzem magam az órákon.</Label>
              <RadioGroup value={q14} onValueChange={setQ14} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q14-${v}`} value={v} />
                    <Label htmlFor={`q14-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>15) Nagy erőfeszítésembe kerül az otthoni felkészülés, hogy ebből a tárgyból jó eredményeket érjek el.</Label>
              <RadioGroup value={q15} onValueChange={setQ15} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q15-${v}`} value={v} />
                    <Label htmlFor={`q15-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>16) A Tanár figyelembe veszi és betartja a Tanulók Statútumát.</Label>
              <RadioGroup value={q16} onValueChange={setQ16} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q16-${v}`} value={v} />
                    <Label htmlFor={`q16-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>

            <div className="space-y-2">
              <Label>17) Ezen az órán ideges vagyok, gyomoridegem van.</Label>
              <RadioGroup value={q17} onValueChange={setQ17} className="grid grid-cols-5 gap-2">
                {likertValues.map((v) => (
                  <div key={v} className="flex items-center space-x-2">
                    <RadioGroupItem id={`q17-${v}`} value={v} />
                    <Label htmlFor={`q17-${v}`}>{v}</Label>
                  </div>
                ))}
              </RadioGroup>
            </div>
          </div>
        </section>

        {/* ISKOLÁN KÍVÜLI OKTATÁS */}
        <section className="space-y-6">
          <header>
            <h2 className="text-xl font-semibold">Iskolán kívüli oktatás</h2>
          </header>

          <div className="space-y-2">
            <Label>18) A Tanár részesít külön foglalkozásban, hogy felkészítsen vizsgára/versenyre/szereplésre:</Label>
            <RadioGroup value={q18} onValueChange={setQ18} className="grid gap-2">
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q18-elkereztetve" value="elkereztetve" />
                <Label htmlFor="q18-elkereztetve">igen, elkéreztetve más Tanárok óráiról</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q18-iskidon" value="iskidon" />
                <Label htmlFor="q18-iskidon">igen, iskolaidőn kívül</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q18-nincs" value="nincs" />
                <Label htmlFor="q18-nincs">nincs külön foglalkozás ebből a tantárgyból</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q18-egyeb" value="egyeb" />
                <Label htmlFor="q18-egyeb">egyéb, éspedig:</Label>
              </div>
            </RadioGroup>
            {q18 === "egyeb" && (
              <div className="pt-2">
                <Input value={q18Other} onChange={(e) => setQ18Other(e.target.value)} placeholder="Írd le röviden..." />
              </div>
            )}
          </div>

          <div className="space-y-2">
            <Label>19) Ebből a tantárgyból iskolán kívül:</Label>
            <RadioGroup value={q19} onValueChange={setQ19} className="grid gap-2">
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q19-maganora" value="maganorak" />
                <Label htmlFor="q19-maganora">magánórára, egyéni felkészítőre járok</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q19-csoportos" value="csoportos" />
                <Label htmlFor="q19-csoportos">csoportos felkészülésen veszek részt</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q19-nem" value="nem" />
                <Label htmlFor="q19-nem">nem veszek részt iskolán kívüli oktatásban ebből a tantárgyból</Label>
              </div>
            </RadioGroup>
          </div>

          {isAttendingOutside && (
            <div className="space-y-2">
              <Label>20) Az iskolán kívüli oktatáson azért veszek részt, mert:</Label>
              <div className="grid gap-2">
                {[
                  { id: "tetszik", label: "nagyon tetszik a téma, el szeretnék mélyülni még jobban az ismeretekben" },
                  { id: "lemaradas", label: "szükségesnek érzem, mert nagyon le vagyok maradva az osztáytársakhoz képest" },
                  { id: "nemEleg", label: "úgy érzem, hogy az iskolai oktatás/felkészítés nem elég a vizsgák sikerességéhez/jó jegyek eléréséhez" },
                  { id: "szulok", label: "a szüleim ragaszkodnak hozzá, hogy magánórára járjak" },
                  { id: "szabadido", label: "túl sok a szabadidőm, nincs mivel kitöltsem" },
                ].map((opt) => (
                  <div key={opt.id} className="flex items-center space-x-2">
                    <Checkbox
                      id={`q20-${opt.id}`}
                      checked={q20.includes(opt.id)}
                      onCheckedChange={(checked) =>
                        toggleMulti(opt.id, (updater) => setQ20(updater))
                      }
                    />
                    <Label htmlFor={`q20-${opt.id}`}>{opt.label}</Label>
                  </div>
                ))}
              </div>
              <p className="text-xs text-muted-foreground">Válassz legalább egyet.</p>
            </div>
          )}

          <div className="space-y-2">
            <Label>21) Szeretném, ha ebből a tantárgyból:</Label>
            <div className="grid gap-2">
              {[
                { id: "gyakorlat", label: "gyakorlati szempontok szerint is megközelítenénk órákon a tananyagot" },
                { id: "kevesebbHf", label: "kevesebb házifeladat lenne" },
                { id: "kedvesebb", label: "kedvesebb/barátibb lenne a tanárunk" },
                { id: "tobbInfo", label: "több információt kapnék, ami felhasználhatnék a mindennapokban is" },
                { id: "elegedett", label: "Teljesen elégedett vagyok a mostani helyzettel" },
              ].map((opt) => (
                <div key={opt.id} className="flex items-center space-x-2">
                  <Checkbox
                    id={`q21-${opt.id}`}
                    checked={q21.includes(opt.id)}
                    onCheckedChange={() => toggleMulti(opt.id, (updater) => setQ21(updater))}
                  />
                  <Label htmlFor={`q21-${opt.id}`}>{opt.label}</Label>
                </div>
              ))}
            </div>
            <p className="text-xs text-muted-foreground">Válassz legalább egyet.</p>
          </div>
        </section>

        {/* Szöveges kérdések */}
        <section className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="q22">22) Kérünk, fogalmazd meg röviden, mi volt a legjobb ezen az órán?</Label>
            <Textarea id="q22" value={q22} onChange={(e) => setQ22(e.target.value)} placeholder="Rövid válasz..." />
          </div>
          <div className="space-y-2">
            <Label htmlFor="q23">23) Kérünk, fogalmazd meg röviden, mi nem tetszett ezen az órán?</Label>
            <Textarea id="q23" value={q23} onChange={(e) => setQ23(e.target.value)} placeholder="Rövid válasz..." />
          </div>
        </section>

        {/* Jelenlét és elmaradt tanórák */}
        <section className="space-y-6">
          <header>
            <h2 className="text-xl font-semibold">Jelenlét és elmaradt tanórák</h2>
          </header>

          <div className="space-y-2">
            <Label>24) Ebben a tanévben jelen voltam a tantárgyban megtartott:</Label>
            <RadioGroup value={q24} onValueChange={setQ24} className="grid gap-2 md:grid-cols-2">
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q24-25" value="25" />
                <Label htmlFor="q24-25">órák 25%-án</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q24-50" value="50" />
                <Label htmlFor="q24-50">órák 50%-án</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q24-75" value="75" />
                <Label htmlFor="q24-75">órák 75%-án</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q24-90" value=">90" />
                <Label htmlFor="q24-90">órák több mint 90%-án</Label>
              </div>
            </RadioGroup>
          </div>

          <div className="space-y-2">
            <Label>25) Válaszd ki a tantárgyra vonatkozó helyes megállapítást:</Label>
            <RadioGroup value={q25} onValueChange={setQ25} className="grid gap-2 md:grid-cols-2">
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q25-25" value="<=25" />
                <Label htmlFor="q25-25">az órák legfeljebb 25%-a volt megtartva</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q25-50" value="<=50" />
                <Label htmlFor="q25-50">az órák legfeljebb 50%-a volt megtartva</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q25-75" value="<=75" />
                <Label htmlFor="q25-75">az órák legfeljebb 75%-a volt megtartva</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q25-90" value=">=90" />
                <Label htmlFor="q25-90">az órák legalább 90%-a meg volt tartva</Label>
              </div>
            </RadioGroup>
          </div>

          <div className="space-y-2">
            <Label>26) Válaszd ki a gyakoribb megállapítást arra az esetre, ha a Tanárod nem tudta megtartani az órát:</Label>
            <RadioGroup value={q26} onValueChange={setQ26} className="grid gap-2 md:grid-cols-2">
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q26-helyettes" value="helyettes" />
                <Label htmlFor="q26-helyettes">volt helyettesítő tanár</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q26-lyukas" value="lyukas" />
                <Label htmlFor="q26-lyukas">nem volt helyettesítés, lyukas óra lett belőle</Label>
              </div>
              <div className="flex items-center space-x-2">
                <RadioGroupItem id="q26-atren" value="atren" />
                <Label htmlFor="q26-atren">átrendeződött az órarend, így egy órával később/hamarabb mentünk/jöttünk az iskolából</Label>
              </div>
            </RadioGroup>
          </div>
        </section>

        <div className="flex gap-3">
          <Button variant="secondary" onClick={onSaveDraft}>Piszkozat mentése</Button>
          <Button variant="default" onClick={onSubmit}>Beküldés</Button>
        </div>
      </CardContent>
    </Card>
  );
}
