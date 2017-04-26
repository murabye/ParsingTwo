using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace ConsoleApplication53
{
       // аддс 
    /*
CLAUSES
	adds(H1,"1",H1,"1",REZSTR) :-	
		concat("2*",H1,REZSTR).
	adds(H1,T1,H1,T1,REZSTR) :-	
		concat("2*",H1,OUT1),
		concat(OUT1,"*",OUT2),
		concat(OUT2,T1,REZSTR).
	adds(H1,T1,H1,T2,REZSTR) :- addconcatabc(H1,T1,T2,REZSTR).	
	adds(H1,T1,H2,T1,REZSTR) :- addconcatabc(T1,H1,H2,REZSTR).	
	adds(H1,T1,H2,H1,REZSTR) :- addconcatabc(H1,T1,H2,REZSTR).	
	adds(H1,T1,T1,T2,REZSTR) :- addconcatabc(T1,H1,T2,REZSTR).	
	adds(H1,"1",H2,T2,REZSTR) :- 
		concat(H1,"+",OUT1),
		concat(OUT1,H2,OUT2),
		concat(OUT2,"*",OUT3),
		concat(OUT3,T2,REZSTR).
	adds(H1,T1,H2,"1",REZSTR) :- 
		concat(H1,"*",OUT1),
		concat(OUT1,T1,OUT2),
		concat(OUT2,"+",OUT3),
		concat(OUT3,H2,REZSTR).
	adds(H1,T1,H2,T2,REZSTR) :- 
		concat(H1,"*",OUT1),
		concat(OUT1,T1,OUT2),
		concat(OUT2,"+",OUT3),
		concat(OUT3,H2,OUT4),
		concat(OUT4,"*",OUT5),
		concat(OUT5,T2,REZSTR).
        */
        // аддконкатабц
        /*
	addconcatabc("1",B,C,REZSTR) :-
		concat(B,"+",OUT1),
		concat(OUT1,C,REZSTR).
	addconcatabc(A,B,C,REZSTR) :-
		concat(A,"*",OUT1),
		concat(OUT1,"(",OUT2),
		concat(OUT2,B,OUT3),
		concat(OUT3,"+",OUT4),
		concat(OUT4,C,OUT5),
		concat(OUT5,")",REZSTR).
        */

        // сабс   
        /*
	subs(H1,T1,H1,T1,REZSTR) :- REZSTR = "0".
	subs(H1,T1,H1,T2,REZSTR) :- subconcatabc(H1,T1,T2,REZSTR).  
	subs(H1,T1,H2,T1,REZSTR) :- subconcatabc(T1,H1,H2,REZSTR).	
	subs(H1,T1,H2,H1,REZSTR) :- subconcatabc(H1,T1,H2,REZSTR).
	subs(H1,T1,T1,T2,REZSTR) :- subconcatabc(T1,H1,T2,REZSTR).
    subs(H1,"1", H2, T2, REZSTR) :-
        concat(H1,"-", OUT1),
        concat(OUT1, H2, OUT2),
        concat(OUT2,"*", OUT3),
        concat(OUT3, T2, REZSTR).
    subs(H1, T1, H2,"1", REZSTR) :-
        concat(H1,"*", OUT1),
        concat(OUT1, T1, OUT2),
        concat(OUT2,"-", OUT3),
        concat(OUT3, H2, REZSTR).
    subs(H1, T1, H2, T2, REZSTR) :- 
        concat(H1,"*", OUT1),
        concat(OUT1, T1, OUT2),
        concat(OUT2,"-", OUT3),
        concat(OUT3, H2, OUT4),
        concat(OUT4,"*", OUT5),
        concat(OUT5, T2, REZSTR).
        */
        // сабконкатабц
        /*
    subconcatabc("1", B, C, REZSTR) :-	
        concat(B,"-", OUT1),
        concat(OUT1, C, REZSTR).
    subconcatabc(A, B, C, REZSTR) :- 
        concat(A,"*", OUT1),
        concat(OUT1,"(", OUT2),
        concat(OUT2, B, OUT3),
        concat(OUT3,"-", OUT4),
        concat(OUT4, C, OUT5),
        concat(OUT5,")", REZSTR).
        */
        
        // экспрешн
        /*
    expression(STR, REZ, OST) :-
        slagaemoe(STR, ADD11, ADD12, OST1),
        operator(OST1,'+', OST2),
        slagaemoe(OST2, ADD21, ADD22, OST),
        adds(ADD11, ADD12, ADD21, ADD22, REZ).
    expression(STR, REZ, OST) :-		
        slagaemoe(STR, ADD11, ADD12, OST1),
        operator(OST1,'-', OST2),
        slagaemoe(OST2, ADD21, ADD22, OST),
        subs(ADD11, ADD12, ADD21, ADD22, REZ).
    expression(STR, REZ, OST) :-
        slagaemoe(STR, REZ,"1", OST).
    expression(STR, REZ, OST) :-		
        WRITE("exp4, "),
        slagaemoe(STR, ADD1, ADD2, OST), 
        concat(ADD1,"*", OUT1),	
        concat(OUT1, ADD2, REZ).
        */
        
        // слагаемое
        /*
    slagaemoe(STR, REZSTR1, REZSTR2, OST) :-	
        mnojitel(STR, REZSTR1, OST1),
        operator(OST1,'*', OST2),
		!,	
        mnojitel(OST2, REZSTR2, OST).
    slagaemoe(STR, REZSTR1,"1", OST) :-	
        mnojitel(STR, FACT1, OST1),
        operator(OST1,'/', OST2),
        mnojitel(OST2, FACT2, OST),
        concat(FACT1,"/", OUT1),
        concat(OUT1, FACT2, REZSTR1).
    slagaemoe(STR, REZSTR1,"1", OST) :-		
        mnojitel(STR, REZSTR1, OST).
        */
        
        // множитель
        /*
    mnojitel(STR, REZSTR, OST) :-		
        FRONTCHAR(STR,'(', OST1),
        expression(OST1, REZ, OST2),
        FRONTCHAR(OST2,')', OST),
        concat("(", REZ, OUT1),
        concat(OUT1,")", REZSTR).
        
    mnojitel(STR, REZSTR, OST) :-	
        FRONTCHAR(STR, CH, OST),
        STR_CHAR(REZSTR, CH).
        */
        
        // оператор
        /*
    operator(STR, CH, OST) :-		
        FRONTCHAR(STR, CH, OST).
    */
    
        // мэин
        /*	
    start :- 
        WRITE("WRITE YOUR EXPRESSION"),
		NL,
        WRITE("CORRECT INPUT"),
		NL,
        WRITE("HELP DESK a*(b-c*b)+(d*e-d*f):"),
		NL,
        READLN(S),
        expression(S, REZ, OST),
        WRITE("RESSULT:"),
		NL,
        WRITE(REZ),
		NL,
		NL.
    */

    class Program
    {
        
        static string s = "", rez = "", ost = "", str = "",
            ch = "", rezstr = "", t1 = "", t2 = "", h2 = "",
            h1 = "", out1 = "", out2 = "", out3 = "", out4 = "",
            a = "", b = "", c = "", add11 = "", add12 = "",
            ost1 = "", ost2 = "", add21 = "", add22 = "",
            res = "", add1 = "", add2 = "", fact1 = "",
            fact2 = "", rezstr1 = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Программа выносит общий множитель за скобку: ");
            Console.WriteLine("Для корректной работы программы у каждого оператора не более 2 операндов");
            Console.WriteLine("Пример: a*(b-c*b)+(d*e-d*f)");
            Console.WriteLine("Введите выражение: ");
            s = Console.ReadLine();
            expression(s, rez, ost);
            Console.WriteLine("Результат выполнения программы: ");
            Console.WriteLine(rez);
        }
        static bool expression(string str, string rez, string ost) {
            if (slagaemoe(str, add11, add12, ost1) 
                && operatop(ost1, "+", ost2) && 
                slagaemoe(ost2, add21, add22, ost) && 
                adds(add11, add12, add21, add22, rez)) ;

            else if (slagaemoe(str, add11, add12, ost1)
                && operatop(ost1, "-", ost2) &&
                slagaemoe(ost2, add21, add22, ost) &&
                subs(add11, add12, add21, add22, rez)) ;

            else if (slagaemoe(str, rez, "1", ost)) ;

            else if (slagaemoe(str, rez, ost) && 
                concat(add1, "8", out1) && 
                concat(out1, add2, rez)) ;

            else return false;
            return true;
        }

    }
}
