   M S B A M L   `   `   `  ����E  AChatClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:ChatClient
ChatClient  5          ChatClient.Two_factor   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:ChatClient     5        N� Title$  
Two_factor��5	      .��5      � 
������� � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� $��Center=�6   $��1��6/   $��2��6?   $��1��6S   $��2��6`   $��Center=�6q   $��25(�6�   $��0,0,0,0q�6�   97Двухфакторная аутентификация6�   �� 5   
   $��0��6   $��4��6$   $��2��68   $��2��6E   $��Center=�6V   $��Center=�6s   $��25(�6�   $��0,0,0,10q�6�   YWАккаунт защищен двухфакторной аутентификацией6�   �� 5   
   $��0��6   $��4��6$   $��3��68   $��Center=�6F   $��Center=�6c   $��25(�6~   $��0,0,0,0q�6�   [YНа вашу почту отправлен код доступа. Введите его:6�   �� 5!   
   -   5         ��Name$ 
backButton��5!      $��3��6%   $��7��65   $&�� Выйти из аккаунта��6B   $��Right=�6^   $	��150��6z   $��0,50,0,0q�6�   $��15(�6�   ���	#FFE6E6E66�   �� 5"   
   -   5         ��Name$ textBox1��5"      $��4��6$   $��2��61   $��1��6B   $��2��6R   $��Center=�6f   $��Center=�6�   $��0,0,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $�� ��6�   $	��350��6�   $��40��6�   $��24(�6�   �� 5#   
   -   5        $ button1��5#      $��1��6"   $��2��62   $��5��6F   $��2��6S   $��Ввод��6d   $��Center=�6s   $��Center=�6�   $��0,0,0,0q�6�   $��35��6�   $	��250��6�   $��23(�6�   ���	#FFE6E6E66�   5%      5&      