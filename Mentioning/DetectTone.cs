#pragma warning disable CS8602 // Dereference of a possibly null reference.

using System.Diagnostics;

namespace Dahlia.Mentioning;

static class DetectTone
{

    public static void Init()
    {
        
    }

    public static string Predict(string message)
    {
        //keys
        string[] aggressiveKeys = File.ReadAllLines("Mentioning/tone-keys/aggressive");
        string[] loveKeys = File.ReadAllLines("Mentioning/tone-keys/love");

        string[] dahliaKeys = File.ReadAllLines("Mentioning/addressing-keys/dahlia");
        string[] userKeys = File.ReadAllLines("Mentioning/addressing-keys/user");

        string[] questionKeys = File.ReadAllLines("Mentioning/type-keys/question");
        string[] statementKeys = File.ReadAllLines("Mentioning/type-keys/statement");

        char[] acceptableCharacters = ['a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'];

        //inputs
        string[] messageSplit = message.ToLower().Split(' ');

        //sentiments
        float tone = 0;
        float addressing = 0;
        float type = 0;
        foreach (string split in messageSplit)
        {
            string processedSplit = "";
            foreach (char character in split.ToCharArray())
            {
                bool accepted = false;
                foreach (char acceptableChar in acceptableCharacters)
                {
                    if(character == acceptableChar) accepted = true;
                }
                if(accepted ) processedSplit += character;
            }
            if(processedSplit == "dahlia") processedSplit = "";

            foreach (string aggressive in aggressiveKeys) {if(aggressive == processedSplit) tone += 0.2f;}
            foreach (string love in loveKeys) {if(love == processedSplit) tone -= 0.2f;}

            foreach (string dahlia in dahliaKeys) {if(dahlia == processedSplit) addressing += 0.2f;}
            foreach (string user in userKeys) {if(user == processedSplit) addressing -= 0.2f;}

            foreach (string question in questionKeys) {if(question == processedSplit) type += 0.2f;}
            foreach (string statement in statementKeys) {if(statement == processedSplit) type -= 0.2f;};
        }

        Tones finalTone = Tones.neutral;
        if(tone > 0.1) finalTone = Tones.aggressive;
        if(tone < -0.1) finalTone = Tones.lovely;

        Addressing finalAddressing = Addressing.dahlia;
        if(addressing > 0) finalAddressing = Addressing.dahlia;
        if(addressing < 0) finalAddressing = Addressing.user;

        Types finalType = Types.statement;
        if(type > 0) finalType = Types.question;
        if(type < 0) finalType = Types.statement;

        return SelectResponse(finalTone, finalAddressing, finalType);
    }

    public static string SelectResponse(Tones tone, Addressing addressing, Types type)
    {

        Random rng = new();
        string[] responses = [];
        string response = "";

        switch (tone)
        {
            case Tones.aggressive:
                switch (type)
                {
                    case Types.question:
                        switch (addressing)
                        {
                            case Addressing.dahlia:
                                responses = File.ReadAllLines("Mentioning/responses/aggressive/question/dahlia");
                                response = responses[rng.Next(0,responses.Length)];
                                break;

                            case Addressing.user:
                                responses = File.ReadAllLines("Mentioning/responses/aggressive/question/user");
                                response = responses[rng.Next(0,responses.Length)];
                                break;
                                
                        }
                        break;

                    case Types.statement:
                        switch (addressing)
                        {
                            case Addressing.dahlia:
                                responses = File.ReadAllLines("Mentioning/responses/aggressive/statement/dahlia");
                                response = responses[rng.Next(0,responses.Length)];
                                break;

                            case Addressing.user:
                                responses = File.ReadAllLines("Mentioning/responses/aggressive/statement/user");
                                response = responses[rng.Next(0,responses.Length)];
                                break;
                                
                        }
                        break;

                }
                break;

            case Tones.lovely:
                switch (type)
                {
                    case Types.question:
                        switch (addressing)
                        {
                            case Addressing.dahlia:
                                responses = File.ReadAllLines("Mentioning/responses/lovely/question/dahlia");
                                response = responses[rng.Next(0,responses.Length)];
                                break;

                            case Addressing.user:
                                responses = File.ReadAllLines("Mentioning/responses/lovely/question/user");
                                response = responses[rng.Next(0,responses.Length)];
                                break;
                                
                        }
                        break;

                    case Types.statement:
                        switch (addressing)
                        {
                            case Addressing.dahlia:
                                responses = File.ReadAllLines("Mentioning/responses/lovely/statement/dahlia");
                                response = responses[rng.Next(0,responses.Length)];
                                break;

                            case Addressing.user:
                                responses = File.ReadAllLines("Mentioning/responses/lovely/statement/user");
                                response = responses[rng.Next(0,responses.Length)];
                                break;
                                
                        }
                        break;

                }
                break;

            case Tones.neutral:
                switch (type)
                {
                    case Types.question:
                        switch (addressing)
                        {
                            case Addressing.dahlia:
                                responses = File.ReadAllLines("Mentioning/responses/neutral/question/dahlia");
                                response = responses[rng.Next(0,responses.Length)];
                                break;

                            case Addressing.user:
                                responses = File.ReadAllLines("Mentioning/responses/neutral/question/user");
                                response = responses[rng.Next(0,responses.Length)];
                                break;
                                
                        }
                        break;

                    case Types.statement:
                        switch (addressing)
                        {
                            case Addressing.dahlia:
                                responses = File.ReadAllLines("Mentioning/responses/neutral/statement/dahlia");
                                response = responses[rng.Next(0,responses.Length)];
                                break;

                            case Addressing.user:
                                responses = File.ReadAllLines("Mentioning/responses/neutral/statement/user");
                                response = responses[rng.Next(0,responses.Length)];
                                break;
                                
                        }
                        break;

                }
                break;
        }

        return response;
    }

    public enum Tones
    {
        lovely,
        neutral,
        aggressive
    }
    
    public enum Addressing
    {
        dahlia,
        user
    }

    public enum Types
    {
        question,
        statement
    }
}