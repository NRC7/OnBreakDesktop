   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullZ=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.MetroMahApps.Metro.Behaviours  5   	   V;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   G CPresentacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:OnBreakApp
OnBreakApp 5	   	      OnBreakApp.Login   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   N
behaviours=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.Metro  5   	   JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   $localclr-namespace:OnBreakApp  5	   	     � Title$%  OnBreak - Eventos Empresariales��5   	   
���   �$	��600��6D   $	��800��6Q    � WindowStartupLocation$ CenterScreen=�6]   .��5      �  � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      $	��40* �6   �� 5      $
��357* �6   .+�5   
   �� $
��Left=�6   $	��325��6+   $��	10,90,0,0q�68   $	��Top=�6K   $	��360��6c   $
��Fill=�6o   $��Images/logo.png��6~   $��3��6�   �� 5   
   $
��Left=�6   $	��106��6+   $��310,124,0,0q�68   $	��Top=�6M   $	��105��6e   $
��Fill=�6q   $��Icons/User-Login.png��6�   $��2��6�   �� 5   
   $��Rut Usuario��6   $��Center=�6&   $��420,120,105,0q�6C   $	��Top=�6Z   $	��189��6�   
�������$��Center=�6�   $��18(�6�   $��2��6�   ��6r   Z� 6�   �� 5   
   -   5         ��Name$ txtUser��5      $
��Left=�6#   $��46��6>   *   $MahApps.Metro.Controls.TextBoxHelper   ClearTextButton .    	Watermark$  ��5      $��420,159,0,0q�5       �� TextWrapping$
 Wrap=�6'   $	��Top=�6;   $	��189��6S   $��24(�5      $��2��6!   ���	#FFF3EBEB61   �� 5    
   $
��Left=�6   $	��100��6+   $��305,275,0,0q�68   $	��Top=�6M   $	��105��6e   $
��Fill=�6q   $��Icons/Login-02.png��6�    J� RenderTransformOrigin$ 
0.524,0.15&�6�   $��2��6�   �� 5!   
   $��Contraseña��6   $��Center=�6%   $��415,261,110,0q�6B   $	��Top=�6Y   $	��189��6�   
�������$��Center=�6�   $��18(�6�   $��2��6�   ��6q   Z� 6�   G� 5"   
   -   5        $��
pbPassword��5"      $
��Left=�6(   $��46��6C    . $  ��5#      $��420,300,0,0q�5$      $	��Top=�6'   $	��189��6?   $��24(�5%      $��2��6    ���	#FFF3EBEB60   �� 5(   
   -   5         ��Name$ btnIngresar��5(      $��Ingresar��6&   
�������$
��Left=�6L   $��450,441,0,0q�6g   $	��Top=�6|   $	��140��6�   ���	#FF27B820$��SemiBold"�6�   $��18(�6�   $��2��6�   5*      5+      