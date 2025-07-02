import { useNavigate } from 'react-router-dom'
import WelcomeHeader from '../../Components/WelcomeHeader/WelcomeHeader'
import WelcomeBody from '../../Components/WelcomeBody/WelcomeBody'
import './LandingPage.css'

function LandingPage() {
  const navigate = useNavigate()

  const login = (): void => {
    navigate('/login');
  };

  const register = (): void => {
    navigate('/register');
  };

  return (
    <>
      <WelcomeHeader login={login} register={register} />
      <WelcomeBody />
    </>
  )
}

export default LandingPage
