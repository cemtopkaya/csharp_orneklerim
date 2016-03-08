using System;
using System.Speech.Synthesis;
using System.Threading;

namespace CSharp_Orneklerim.Threading_Event
{
    public class Threading_Event_SpeechSynthesizer
    {
        public static void Calis()
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10

            // Synchronous
            synthesizer.Speak("Merhaba Cem");
            synthesizer.SpeakCompleted += (a, b) =>
                                          {
                                              Console.WriteLine("Konuşma tamamlandı");
                                          };

            // Asynchronous
            var p = synthesizer.SpeakAsync("Asynchronous talk with Speak Async");
            while (!p.IsCompleted)
            {
                Console.WriteLine("Async function is still continuing");
                Thread.Sleep(100);
            }
            Console.WriteLine("End of Main Thread");
        }
    }
}
