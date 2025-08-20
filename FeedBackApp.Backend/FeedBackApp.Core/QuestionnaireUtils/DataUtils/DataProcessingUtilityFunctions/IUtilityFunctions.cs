using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackApp.Core.QuestionnaireUtils.DataUtils.DataProcessingUtilityFunctions
{
    internal interface IUtilityFunctions
    {
        // LIKERT-SCALE
        // functions:

        // kiszamitja egy adott lista (int-s) elemeinek atlagat
        public Task<double> CalculateMean(IEnumerable<int> numberList);
        // kiszamitja egy szamsor kozponti tendenciajat, medianjat (az adatok rendezettek kell legyenek)
        // ha darabszam paratlan → a kozepso ertek
        // ha darabszam paros → a ket kozepso atlaga
        public Task<double> CalculateMedian(IEnumerable<int> numberList);

        // modusz, a leggyakoribb elem
        public Task<int> CalculateMode(IEnumerable<int> numberList);

        // nagy szoras → nagy velemeny kulonbseg
        public Task<double> CalculateStandardDeviation(IEnumerable<int> numberList);

        public Task<int> CalculateMaximumRate(IEnumerable<int> numberList);

        public Task<int> CalculateMinimumRate(IEnumerable<int> numberList);

        //mennyire a pozitiv/negativ visszajelzesek aranya
        public Task<double> CalculateAgreementRate(IEnumerable<int> numberList);

        // atlag ertek aranya a maximalis elerheto ertekhez kepest.
        public Task<double> CalculateSatisfactionIndex(IEnumerable<int> numberList);

        // azt jelenti hogy tobb ugyanazt a dimenziot mero kerdes mennyire megbizhato, Cronbach's alpha
        public Task<double> CalculateInternalConsistency(int numberOfQuestions, IEnumerable<double> variancesOfEachQuestion, double aggregatedVariance);

        // Single-Choice questions
        // functions:

        // megmutatja, hogy az egyes valaszlehetosegeket hanyan valasztottak, es az osszesbol ez hany szazalek
        // kiszamolom minden kategoriara hogy hany valasz tartozik oda
        // szazalek = adott kategoria valaszainak szama/ osszes valasz x 100%
        public Task<double> CalculateFrequencyDistribution();
        /*
         minden csopin belul kiszamolom a gyakorisagi eloszlast.
        pl. matekbol tobb diak jar kulonorara mint tornabol pl.
         */
        public Task<double> CalculateRelativeProportionAcrossGroups();

        // KEDVENCEM!!!!
        /*
         lathato lesz, hogy sokat hianyzok/gyengebb teljesitmenyu tanulok alacsonyabb elegedettseget mutatnak
         */
        public Task<double> CalculateCrossTabulations();

        //  MULTIPLE-CHOICE QUESTIONS

        //megszmaoljuk hogy egy adott valaszt hanyszor jeloltek be
        public Task<double> CalculateFrequencyForEachResponseOptions();

        // megnezzuk hogy milyen valaszok fordulnak elo gyakran egyutt
        public Task<double> CalculateCooccurenceAnalysis();

        // az atfedeseket vizualisan mutatjuk meg
        public Task<double> ComposeStackedBar();

        // OPEN-ENDED QUESTIONS

        // hat...ez munkasabb, majd meglatjuk, de:...
        // kulcsszo gyakorisag
        //pozitiv-negativ arany
        //reprezentativ idezetek
        //

    }
}
