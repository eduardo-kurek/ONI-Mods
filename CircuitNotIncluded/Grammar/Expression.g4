grammar Expression;

// Lexer rules
NOT: '!';
AND: '*';
XOR: '#';
OR: '+';
LPAREN: '(';
RPAREN: ')';

TRUE: 'true';
FALSE: 'false';
ID: [_a-zA-Z]+[_a-zA-Z0-9]*;

WS: [ \t]+ -> skip;
ERROR : . ;

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
    : ID                            #idFactor
    | TRUE                          #trueFactor
    | FALSE                         #falseFactor
    | LPAREN expression RPAREN      #parFactor
    ;