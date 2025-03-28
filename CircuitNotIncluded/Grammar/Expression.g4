grammar Expression;

// Lexer rules
OR: '|'; 
XOR: '+';
AND: '&';
NOT: '!';
ID: [_a-zA-Z]+[_a-zA-Z0-9]*;
LPAREN: '(';
RPAREN: ')';

// Parser rules
program: expression EOF;

expression
    : factor                        #factorExpresssion
    | NOT factor                    #notExpresssion
    | expression AND expression     #andExpresssion
    | expression XOR expression     #xorExpresssion
    | expression OR expression      #orExpresssion
    ;

factor
    : LPAREN expression RPAREN      #parFactor
    | ID                            #idFactor
    ;

WS: [ \t]+ -> skip;
