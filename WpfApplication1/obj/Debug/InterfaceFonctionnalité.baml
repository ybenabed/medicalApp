   M S B A M L   `   `   `  ����  WpfApplication1.    (WpfApplication1.InterfaceFonctionnalité   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  InterfaceFonctionnalité��5   	   $	��690��6)   $
��1080��66    � 
ResizeMode$ NoResize=�6C    � WindowStartupLocation$ CenterScreen=�6Y    � WindowStyle$
 None=�6~   .��5      � .+�5   
   �� -   5        $��	MainStack��5   -   
�������.+�5      � -   5        $��maingrd��5   4   $��40��6   ���#0c4a60.+�5	      �� $
��Left=�6   $��0q�68   �� 5
       �� Orientation$ 
Horizontal=�6   $��Right=�66   $��10,0,1020,0q�6R   .+�5      �� -   5        $��Center=�5   a   ��6   Z� ��63   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ Logout=�62   ���	#FFA2A2A2$��25��6W   $��25��6b    J� RenderTransformOrigin$ 0.5,0.5&�6n   z�5      V� 5   "   .~�5   &   �� �� 5   &   �� 5   &    �� Angle$	 180Q�66   T� 5   &   5   #   5      5      5      5      � 5      $��60��6   .+�5      �� $ 
Horizontal=�6   $	��Top=�66   $
��10 0q�6N   .+�5      �� -   5        
 J� Uid$ 0��5      $	��200��6%   $��FICHE DE CONSULTATION��61   $��50��6Q   ���#0c4a60��6]   Z� ��6s   Z� 6�   �� 5      -   5        $ 1��5      $	��200��6%   $��EXAMEN COMPLEMENTAIRE��61   $��50��6Q   ���#0c4a60��6]   Z� ��6s   Z� 6�   �� 5      -   5        $ 2��5      $	��200��6%   $��
ORDONNANCE��61   $��50��6F   ���#0c4a60��6R   Z� ��6h   Z� 6�   �� 5      -   5        $ 3��5      $	��200��6%   $��
CERTIFICAT��61   $��50��6F   ���#0c4a60��6R   Z� ��6h   Z� 6�   �� 5      -   5        $ 4��5      $	��200��6%   $��ORIENTATION��61   $��50��6G   ���#0c4a60��6S   Z� ��6i   Z� 6�   5       � 5!      -	   5        	 �Name$	 
GridCursor��5!      $	��200��6+   $��5��67   ���	#FFD54A42$
��Left=�6Y   $��
10,45,0,10q�6t   5"      � 5#      -
   5        $	 GridMain��5#      $	��589��6%   
�������.+�5$       � -   5        $��support_fonction��5$      $��0q�60   
  � NavigationUIVisibility$
 Hidden=�6;   $	��Top=�6[   5%      5&      �� 5'   
   -   5        $��RecinactivityMessage��5'      $��Center=�60   $��Center=�6M   $	��350��6h   $��70��6t   $��365,310q�6�   
���  ��$y�Hidden=�6�    J� Effect 5(      3  -System.Windows.Media.Effects.DropShadowEffect  5)         
BlurRadius$ 15Q�6#      	Direction$ 0Q�63      RenderingBias$ Quality=�6A      ShadowDepth$ 4Q�6Y      Color$ 	#FFBBBBBB��6i   5+      �� 5,   
   -   5        $��inactivityMessage��5-      $��Center=�5,      $��Center=�62   $��70��6M   $	��350��6Y   $y�Hidden=�5-   .   .+�5.      �� $ 
Horizontal=�6   $��Center=�62   .+�5/      �� $,��&Votre session se déconnectera aprés ��6   $��14(�6G   
�������$��SemiBold"�6h   ��6~   Z� �� 50      -   5        $��minuteur��50      $��14(�6(   
�������$��SemiBold"�6I   $��-8,0,0,0q�6_   �� 51      $��
seconde(s)��6   $��14(�6-   
�������$��SemiBold"�6N   $��-8,0,0,0q�6d   52      �� 53      -   5        $��annuler��53   '   $��Annuler��6   $��0,3,0,0q�66   $��90��6G   
���������6h   Z� ��6~   Z� 6�   54      55      56      