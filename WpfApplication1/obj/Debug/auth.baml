   M S B A M L   `   `   `  ����  WpfApplication1    WpfApplication1.auth   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$
  auth��5   	   $	��500��6   $	��350��6#    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   $��Segoe UI Emoji)�5	   	   .��5
      � .+�5   
   � -   5        $��authgrd��5      .+�5      �� $	��280��6   $	��Top=�6%   ���#0c4a605      �� 5      $	��280��6   $	��240��6$   $��Bottom=�61   $
��0,80q�6L    �� RadiusY$ 10��6Z    �� RadiusX$ 10��6g   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6'      	Direction$ 0Q�67   	   RenderingBias$	 Quality=�6E   
   ShadowDepth$
 1Q�6]      Color$ 	#FFBBBBBB��6m   5      � 5      $��Bottom=�6   $��35,80q�6.   $	��240��6=   .+�5      �� $��	CONNEXION��6   $��Center=�6,   $��84,5,84,201q�6I   
�������$��18(�6p   �� 5      $��Center=�6   $��15q�68   .+�5      �� -   5         ��Name$ Username��5      $
��0,10q�60   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ NOM D'UTILISATEUR��6>   $��Champagne & Limousines)�6q    �� 	MaxLength$ 20��5      $��18(�6-   G� 5      -   5         G�Name$ Password��5   "   $
��0,10q�64   $ MOT DE PASSE��6B   $��Champagne & Limousines)�6p   $��18(�6�    G� 	MaxLength$ 20��6�   6�   5      5      �� 5      -   5         ��Name$ log_butt��5      $	��150��6'   $��Center=�63   $��Bottom=�6P   $
��0,65q�6k   $��SE CONNECTER��6y   ���	#FFD54A42���	#FFD54A426�   �� 5      -   5         ��Name$ 	rect_auth��5      $
��Left=�6+   $��25��6F   $	��Top=�6R   $	��350��6j   ���	#FF0834445       �� 5!      -   5         ��Name$ ic_close��5!      $��326,0,0,476q�6&   $'��!Images\ic_close_white_24dp_1x.png��6;   $
��Fill=�6f    � Cursor$
 Hand��6�   �� 5"      -   5        $ 
ic_reduire��5"      $��302,0,24,476q�6(   $(��"Images\ic_remove_white_24dp_1x.png��6>   $
��Fill=�6j   $
 Hand��6�   �� 5#      $��106,48,118,342q�6   $��med.png��6,   $
��Fill=�6=   �� 5$      -   5        $��5,0,255,474q�5$      $��	logo5.png��6)   $
��Fill=�6<   6K   5%      �� 5&   
   -	   5        $��Reccptdsctvé��5&      $��94,264,87,177q�6(   $y�Hidden=�6@   
������� 5'        5(      $ 15Q�6#   $ 0Q�63   $	 Quality=�6A   $
 1Q�6Y   $ 	#FFBBBBBB��6i   5*      �� 5+   
   -
   5        $��
cptdsctvé��5+   ,    �� Orientation$ Vertical=�6   $��Center=�6=   $��Center=�6Z   $��94,264,87,177q�6u   $y�Hidden=�6�   .+�5,      �� $#��Votre compte est désactivé!��6   $��13(�6:   �� 5-      -   5        $��buttok��5-      $��ok��6#   $��Center=�60   ���	#FFD54A42��6d   Z� ��6z   Z� 6�   5.      �� 5/   
   -   5        $��RecStackerrauth��5/      $��103,270,109,170q�6+   $y�Hidden=�6E   
������� 50        51      $ 15Q�6#   $ 0Q�63   $	 Quality=�6A   $
 1Q�6Y   $ 	#FFBBBBBB��6i   53      �� 54   
   -   5        $��Stackerrauth��54      $ Vertical=�6)   $��Center=�6@   $��Center=�6]   $��103,270,109,170q�6x   $y�Hidden=�6�   $��60��6�   .+�55      �� $��Erreur d'authentification��6   �� 56      -   5        $��	buttokerr��56   "   $��ok��6   $��Center=�63   ���	#FFD54A42��6g   Z� ��6~   Z� 6�   57      58      59      